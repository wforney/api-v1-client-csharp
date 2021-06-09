namespace Info.Blockchain.API.Json
{
	using Newtonsoft.Json;

	using System;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// The true trumps all JSON converter class. Implements the <see cref="JsonConverter" />.
	/// </summary>
	/// <seealso cref="JsonConverter" />
	internal class TrueTrumpsAllJsonConverter : JsonConverter
	{
		/// <inheritdoc />
		public override bool CanConvert(Type objectType) => objectType == typeof(bool);

		/// <inheritdoc />
		[SuppressMessage("Readability", "RCS1238:Avoid nested ?: operators.", Justification = "This is simple enough to ignore this rule.")]
		public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) =>

			// value always true if true already
			existingValue is bool x && x ? true : reader.Value is bool boolean ? boolean : (object)false;

		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => writer.WriteValue(value);
	}
}
