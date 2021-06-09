namespace Info.Blockchain.API.Json
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Text.RegularExpressions;

	/// <summary>
	/// The UnixEpochDateTimeConverter class. This class cannot be inherited. Implements the <see
	/// cref="JsonConverter{DateTime}" />.
	/// </summary>
	/// <seealso cref="JsonConverter{DateTime}" />
	public sealed class UnixEpochDateTimeConverter : JsonConverter<DateTime>
	{
		private static readonly Regex DateRegex = new Regex(@"^Date\([+-]*(\d+)\)$", RegexOptions.CultureInvariant);
		private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

		/// <inheritdoc />
		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			string? formatted;
			if (reader.TokenType == JsonTokenType.Number)
			{
				formatted = reader.GetDecimal().ToString(CultureInfo.CurrentCulture);
			}
			else
			{
				formatted = reader.GetString();
			}

			if (formatted is null)
			{
				throw new JsonException();
			}

			if (this.IsNumeric(formatted))
			{
				formatted = $"Date({formatted})";
			}

			var match = DateRegex.Match(formatted);

			if (!match.Success
				|| !long.TryParse(match.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out long unixTime))
			{
				throw new JsonException();
			}

			return Epoch.AddMilliseconds(unixTime);
		}

		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		{
			var unixTime = Convert.ToInt64((value - Epoch).TotalMilliseconds);

			var formatted = FormattableString.Invariant($"/Date({unixTime})/");
			writer.WriteStringValue(formatted);
		}

		/// <summary>
		/// Determines whether the specified value is numeric.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the specified value is numeric; otherwise, <c>false</c>.</returns>
		private bool IsNumeric(string? value) => value?.All(char.IsNumber) ?? false;
	}
}
