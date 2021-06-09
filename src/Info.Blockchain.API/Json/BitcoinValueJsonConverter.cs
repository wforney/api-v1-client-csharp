namespace Info.Blockchain.API.Json
{
	using Info.Blockchain.API.Models;

	using Newtonsoft.Json;

	using System;
	using System.Globalization;

	/// <summary>
	/// The bitcoin value JSON converter class. Implements the <see cref="JsonConverter" />.
	/// </summary>
	/// <seealso cref="JsonConverter" />
	internal class BitcoinValueJsonConverter : JsonConverter
	{
		/// <inheritdoc />
		public override bool CanConvert(Type objectType) => objectType == typeof(BitcoinValue);

		/// <inheritdoc />
		public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) =>
			reader.Value is long satoshis ? BitcoinValue.FromSatoshis(satoshis) : BitcoinValue.Zero;

		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) =>
			writer.WriteRawValue(value is BitcoinValue bitcoinValue ? bitcoinValue.Satoshis.ToString(CultureInfo.CurrentCulture) : "0");
	}
}
