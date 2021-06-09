namespace Info.Blockchain.API.Models
{
	using Info.Blockchain.API.Json;

	using Newtonsoft.Json;

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Represents a transaction output.
	/// </summary>
	public sealed class Output
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="Output" /> class from being created.
		/// </summary>
		[JsonConstructor]
		[System.Text.Json.Serialization.JsonConstructor]
		public Output()
		{
		}

		/// <summary>
		/// Address that the output belongs to
		/// </summary>
		[JsonProperty("addr")]
		[System.Text.Json.Serialization.JsonPropertyName("addr")]
		public string Address { get; init; } = string.Empty;

		/// <summary>
		/// Index of the output in a transaction
		/// </summary>
		[JsonProperty("n", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("n")]
		public int N { get; init; }

		/// <summary>
		/// Output script
		/// </summary>
		[JsonProperty("script", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("script")]
		public string Script { get; init; } = string.Empty;

		/// <summary>
		/// Whether the output is spent
		/// </summary>
		[JsonProperty("spent", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("spent")]
		public bool Spent { get; init; }

		/// <summary>
		/// Transaction index
		/// </summary>
		[JsonProperty("tx_index", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("tx_index")]
		public long TxIndex { get; init; }

		/// <summary>
		/// Value of the output
		/// </summary>
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[JsonProperty("value", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("value")]
		public BitcoinValue Value { get; init; } = new(0);
	}
}
