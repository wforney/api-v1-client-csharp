namespace Info.Blockchain.API.Json
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	using System;
	using System.Globalization;

	/// <summary>
	/// The Unix date/time JSON converter class. Implements the <see cref="DateTimeConverterBase" />.
	/// </summary>
	/// <seealso cref="DateTimeConverterBase" />
	public class UnixDateTimeJsonConverter : DateTimeConverterBase
	{
		/// <summary>
		/// The genesis block unix millis
		/// </summary>
		public const long GenesisBlockUnixMillis = 1231006505000;

		/// <summary>
		/// Initializes a new instance of the <see cref="UnixDateTimeJsonConverter" /> class.
		/// </summary>
		public UnixDateTimeJsonConverter()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnixDateTimeJsonConverter" /> class.
		/// </summary>
		/// <param name="convertFromMillis">if set to <c>true</c> [convert from millis].</param>
		public UnixDateTimeJsonConverter(bool convertFromMillis) => this.ConvertFromMillis = convertFromMillis;

		/// <summary>
		/// Gets the genesis block date.
		/// </summary>
		/// <value>The genesis block date.</value>
		public static DateTime GenesisBlockDate { get; } = UnixSecondsToDateTime(1231006505);

		/// <summary>
		/// Gets the epoch.
		/// </summary>
		/// <value>The epoch.</value>
		private static DateTime Epoch { get; } = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// Gets a value indicating whether [convert from millis].
		/// </summary>
		/// <value><c>true</c> if [convert from millis]; otherwise, <c>false</c>.</value>
		private bool ConvertFromMillis { get; }

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			var value = reader.Value;
			if (reader.Value is long v)
			{
				value = (double)v;
			}
			else if (reader.Value is string s)
			{
				if (double.TryParse(s, out var doubleValue))
				{
					value = doubleValue;
				}
			}

			if (value is double dv)
			{
				return this.ConvertFromMillis ? UnixMillisToDateTime(dv) : (object)UnixSecondsToDateTime(dv);
			}

			return null;
		}

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			if (value is DateTime dt)
			{
				var unixTimestamp = DateTimeToUnixSeconds(dt);
				if (this.ConvertFromMillis)
				{
					unixTimestamp *= 1000d;
				}

				writer.WriteRawValue(unixTimestamp.ToString(CultureInfo.CurrentCulture));
			}
		}

		/// <summary>
		/// Dates the time to unix seconds.
		/// </summary>
		/// <param name="dateTime">The date time.</param>
		/// <returns>System.Double.</returns>
		internal static double DateTimeToUnixSeconds(DateTime dateTime) => (dateTime - Epoch).TotalSeconds;

		/// <summary>
		/// Unixes the millis to date time.
		/// </summary>
		/// <param name="unixMillis">The unix millis.</param>
		/// <returns>DateTime.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// unixMillis - No date can be before the genesis block (2009-01-03T18:15:05+00:00)
		/// </exception>
		private static DateTime UnixMillisToDateTime(double unixMillis)
		{
			var dateTime = Epoch.AddMilliseconds(unixMillis);
			return dateTime < GenesisBlockDate
				? throw new ArgumentOutOfRangeException(nameof(unixMillis), "No date can be before the genesis block (2009-01-03T18:15:05+00:00)")
				: dateTime;
		}

		/// <summary>
		/// Unixes the seconds to date time.
		/// </summary>
		/// <param name="unixSeconds">The unix seconds.</param>
		/// <returns>DateTime.</returns>
		private static DateTime UnixSecondsToDateTime(double unixSeconds) => UnixMillisToDateTime(unixSeconds * 1000d);
	}
}
