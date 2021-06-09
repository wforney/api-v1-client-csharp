namespace Info.Blockchain.API.Models
{
	using Info.Blockchain.API.Json;

	using Newtonsoft.Json;

	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Represents an address.
	/// </summary>
	public class Address
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Address"/> class.
		/// </summary>
		[JsonConstructor]
		[System.Text.Json.Serialization.JsonConstructor]
		public Address()
		{
		}

		/// <summary>
		/// Base58Check representation of the address
		/// </summary>
		[JsonProperty("address", Required = Required.Always)]
		[System.Text.Json.Serialization.JsonPropertyName("address")]
		public string? Base58Check { get; init; }

		/// <summary>
		/// Final balance of the address
		/// </summary>
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[JsonProperty("final_balance", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("final_balance")]
		public BitcoinValue? FinalBalance { get; init; }

		/// <summary>
		/// Hash160 representation of the address
		/// </summary>
		[JsonProperty("hash160")]
		[System.Text.Json.Serialization.JsonPropertyName("hash160")]
		public string? Hash160 { get; init; }

		/// <summary>
		/// Total amount received
		/// </summary>
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[JsonProperty("total_received", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("total_received")]
		public BitcoinValue? TotalReceived { get; init; }

		/// <summary>
		/// Total amount sent
		/// </summary>
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[JsonProperty("total_sent", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("total_sent")]
		public BitcoinValue? TotalSent { get; init; }

		/// <summary>
		/// Total count of all transactions of this address
		/// </summary>
		[JsonProperty("n_tx", Required = Required.Always)]
		[System.Text.Json.Serialization.JsonPropertyName("n_tx")]
		public long TransactionCount { get; init; }

		/// <summary>
		/// List of transactions associated with this address
		/// </summary>
		[JsonProperty("txs")]
		[System.Text.Json.Serialization.JsonPropertyName("txs")]
		public IReadOnlyCollection<Transaction>? Transactions { get; init; }
	}
}
