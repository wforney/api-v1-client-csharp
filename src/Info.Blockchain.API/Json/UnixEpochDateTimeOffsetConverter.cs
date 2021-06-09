namespace Info.Blockchain.API.Json
{
	using System;
	using System.Globalization;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Text.RegularExpressions;

	/// <summary>
	/// The UnixEpochDateTimeOffsetConverter class. This class cannot be inherited. Implements the
	/// <see cref="JsonConverter{DateTimeOffset}" />.
	/// </summary>
	/// <seealso cref="JsonConverter{DateTimeOffset}" />
	public sealed class UnixEpochDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
	{
		private static readonly Regex DateRegex = new Regex("^/Date\\(([+-]*\\d+)([+-])(\\d{2})(\\d{2})\\)/$", RegexOptions.CultureInvariant);
		private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

		/// <inheritdoc />
		public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var formatted = reader.GetString();
			if (formatted is null)
			{
				throw new JsonException();
			}

			var match = DateRegex.Match(formatted);

			if (!match.Success
				|| !long.TryParse(match.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out long unixTime)
				|| !int.TryParse(match.Groups[3].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int hours)
				|| !int.TryParse(match.Groups[4].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int minutes))
			{
				throw new JsonException();
			}

			var sign = match.Groups[2].Value[0] == '+' ? 1 : -1;
			var utcOffset = new TimeSpan(hours * sign, minutes * sign, 0);

			return Epoch.AddMilliseconds(unixTime).ToOffset(utcOffset);
		}

		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
		{
			var unixTime = Convert.ToInt64((value - Epoch).TotalMilliseconds);
			var utcOffset = value.Offset;

			var formatted = FormattableString.Invariant($"/Date({unixTime}{(utcOffset >= TimeSpan.Zero ? "+" : "-")}{utcOffset:hhmm})/");
			writer.WriteStringValue(formatted);
		}
	}
}
