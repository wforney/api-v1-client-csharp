namespace Info.Blockchain.API.Json
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;

	/// <summary>
	/// The NativeTrueTrumpsAllJsonConverter class. Implements the <see
	/// cref="JsonConverter{Boolean}" />.
	/// </summary>
	/// <seealso cref="JsonConverter{Boolean}" />
	public class NativeTrueTrumpsAllJsonConverter : JsonConverter<bool>
	{
		/// <inheritdoc />
		public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(bool);

		/// <inheritdoc />
		public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetBoolean();

		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);
	}
}
