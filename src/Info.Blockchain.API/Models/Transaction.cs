namespace Info.Blockchain.API.Models
{
	using Info.Blockchain.API.Json;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Represents a transaction.
	/// </summary>
	public sealed class Transaction
	{
		/// <summary>
		/// Block height of the parent block. -1 for unconfirmed transactions.
		/// </summary>
		[JsonProperty("block_height")]
		[System.Text.Json.Serialization.JsonPropertyName("block_height")]
		public long? BlockHeight { get; init; } = -1;

		/// <summary>
		/// Whether the transaction is a double spend
		/// </summary>
		[JsonProperty("double_spend")]
		[System.Text.Json.Serialization.JsonPropertyName("double_spend")]
		public bool? DoubleSpend { get; init; } = false;

		/// <summary>
		/// Transaction hash
		/// </summary>
		[JsonProperty("hash", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("hash")]
		public string? Hash { get; init; }

		/// <summary>
		/// Transaction index
		/// </summary>
		[JsonProperty("tx_index", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("tx_index")]
		public long Index { get; init; }

		/// <summary>
		/// List of inputs
		/// </summary>
		[JsonProperty("inputs", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("inputs")]
		public IReadOnlyCollection<Input> Inputs { get; init; } = new ReadOnlyCollection<Input>(new List<Input>());

		/// <summary>
		/// List of outputs
		/// </summary>
		[JsonProperty("out", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("out")]
		public IReadOnlyCollection<Output> Outputs { get; init; } = new ReadOnlyCollection<Output>(new List<Output>());

		/// <summary>
		/// IP address that relayed the transaction
		/// </summary>
		[JsonProperty("relayed_by", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("relayed_by")]
		public string? RelayedBy { get; init; }

		/// <summary>
		/// Serialized size of the transaction
		/// </summary>
		[JsonProperty("size", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("size")]
		public long Size { get; init; }

		/// <summary>
		/// Timestamp of the transaction
		/// </summary>
		[JsonProperty("time", Required = Required.Always)]
		[JsonConverter(typeof(UnixDateTimeJsonConverter))]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(UnixEpochDateTimeConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("time")]
		public DateTime Time { get; init; }

		/// <summary>
		/// Transaction format version
		/// </summary>
		[JsonProperty("ver", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("ver")]
		public int Version { get; init; }

		/// <summary>
		/// Deserializes the multiple.
		/// </summary>
		/// <param name="transactionsJson">The transactions json.</param>
		/// <returns>ReadOnlyCollection&lt;System.Nullable&lt;Transaction&gt;&gt;.</returns>
		public static ReadOnlyCollection<Transaction> DeserializeMultiple(string transactionsJson)
		{
			JObject jObject = JObject.Parse(transactionsJson);

			return jObject["txs"]?.ToObject<ReadOnlyCollection<Transaction>>() ?? new ReadOnlyCollection<Transaction>(new List<Transaction>());
		}
	}
}
