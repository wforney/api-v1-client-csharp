namespace Info.Blockchain.API.Models
{
	using Info.Blockchain.API.Json;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Represents an unspent transaction output.
	/// </summary>
	public sealed class UnspentOutput
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="UnspentOutput" /> class from being created.
		/// </summary>
		[JsonConstructor]
		[System.Text.Json.Serialization.JsonConstructor]
		public UnspentOutput()
		{
		}

		/// <summary>
		/// Number of confirmations
		/// </summary>
		[JsonProperty("confirmations", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("confirmations")]
		public long Confirmations { get; init; }

		/// <summary>
		/// Index of the output in a transaction
		/// </summary>
		[JsonProperty("tx_output_n", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("tx_output_n")]
		public int N { get; init; }

		/// <summary>
		/// Output script
		/// </summary>
		[JsonProperty("script", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("script")]
		public string Script { get; init; } = string.Empty;

		/// <summary>
		/// Transaction hash
		/// </summary>
		[JsonProperty("tx_hash", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("tx_hash")]
		public string TransactionHash { get; init; } = string.Empty;

		/// <summary>
		/// Transaction index
		/// </summary>
		[JsonProperty("tx_index", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("tx_index")]
		public long TransactionIndex { get; init; }

		/// <summary>
		/// Value of the output
		/// </summary>
		[JsonProperty("value", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("value")]
		public BitcoinValue Value { get; init; } = new(0);

		/// <summary>
		/// Deserializes multiple unspent outputs.
		/// </summary>
		/// <param name="outputsJson">The outputs JSON.</param>
		/// <returns>System.Collections.ObjectModel.ReadOnlyCollection&lt;Info.Blockchain.API.Models.UnspentOutput&gt;.</returns>
		public static ReadOnlyCollection<UnspentOutput> DeserializeMultiple(string outputsJson)
		{
			JObject jObject = JObject.Parse(outputsJson);
			return jObject["unspent_outputs"]?.ToObject<ReadOnlyCollection<UnspentOutput>>() ?? new ReadOnlyCollection<UnspentOutput>(new List<UnspentOutput>());
		}
	}
}
