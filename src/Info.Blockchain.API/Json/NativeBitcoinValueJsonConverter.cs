namespace Info.Blockchain.API.Json
{
	using Info.Blockchain.API.Models;

	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;

	/// <summary>
	/// The NativeBitcoinValueJsonConverter class. Implements the <see
	/// cref="JsonConverter{BitcoinValue}" />.
	/// </summary>
	/// <seealso cref="JsonConverter{BitcoinValue}" />
	public class NativeBitcoinValueJsonConverter : JsonConverter<BitcoinValue>
	{
		/// <inheritdoc />
		public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(BitcoinValue);

		/// <inheritdoc />
		public override BitcoinValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
			reader.TryGetInt64(out var satoshis) ? BitcoinValue.FromSatoshis(satoshis) : BitcoinValue.Zero;

		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, BitcoinValue value, JsonSerializerOptions options) =>
			writer.WriteNumberValue(value.Satoshis);
	}
}
