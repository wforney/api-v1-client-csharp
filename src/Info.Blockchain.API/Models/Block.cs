namespace Info.Blockchain.API.Models
{
	using Info.Blockchain.API.Json;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	/// <summary>
	/// This class is a full representation of a block. For simpler representations, see <see
	/// cref="SimpleBlock" /> and <see cref="LatestBlock" />.
	/// </summary>
	public sealed class Block : SimpleBlock
	{
		/// <summary>
		/// Gets the received time.
		/// </summary>
		/// <value>The received time.</value>
		private DateTime receivedTime = DateTime.MinValue;

		/// <summary>
		/// Prevents a default instance of the <see cref="Block" /> class from being created.
		/// </summary>
		[JsonConstructor]
		[System.Text.Json.Serialization.JsonConstructor]
		public Block()
		{
		}

		/// <summary>
		/// Representation of the difficulty target for this block
		/// </summary>
		[JsonProperty("bits", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("bits")]
		public long Bits { get; init; }

		/// <summary>
		/// Total transaction fees from this block
		/// </summary>
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[JsonProperty("fee", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("fee")]
		public BitcoinValue Fees { get; init; } = new(0);

		/// <summary>
		/// Index of this block
		/// </summary>
		[JsonProperty("block_index", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("block_index")]
		public long Index { get; init; }

		/// <summary>
		/// Merkle root of the block
		/// </summary>
		[JsonProperty("mrkl_root", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("mrkl_root")]
		public string? MerkleRoot { get; init; }

		/// <summary>
		/// Block nonce
		/// </summary>
		[JsonProperty("nonce", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("nonce")]
		public long Nonce { get; init; }

		/// <summary>
		/// Hash of the previous block
		/// </summary>
		[JsonProperty("prev_block", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("prev_block")]
		public string? PreviousBlockHash { get; init; }

		/// <summary>
		/// The time this block was received by Blockchain.info
		/// </summary>
		[JsonConverter(typeof(UnixDateTimeJsonConverter))]
		[JsonProperty("received_time")]
		[System.Text.Json.Serialization.JsonConverter(typeof(UnixEpochDateTimeConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("received_time")]
		public DateTime ReceivedTime
		{
			get => this.receivedTime == DateTime.MinValue ? this.Time : this.receivedTime;
			private set => this.receivedTime = value;
		}

		/// <summary>
		/// IP address that relayed the block
		/// </summary>
		[JsonProperty("relayed_by")]
		[System.Text.Json.Serialization.JsonPropertyName("relayed_by")]
		public string RelayedBy { get; init; } = "0.0.0.0";

		/// <summary>
		/// Serialized size of this block
		/// </summary>
		[JsonProperty("size", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("size")]
		public long Size { get; init; }

		/// <summary>
		/// Transactions in the block
		/// </summary>
		[JsonProperty("tx", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("tx")]
		public ReadOnlyCollection<Transaction> Transactions { get; init; } = new ReadOnlyCollection<Transaction>(new List<Transaction>());

		/// <summary>
		/// Block version as specified by the protocol
		/// </summary>
		[JsonProperty("ver", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("ver")]
		public int Version { get; init; }

		/// <summary>
		/// Deserializes the specified block json.
		/// </summary>
		/// <param name="blockJson">The block json.</param>
		/// <returns>Info.Blockchain.API.Models.Block?.</returns>
		public static Block? Deserialize(string blockJson)
		{
			var blockJObject = JObject.Parse(blockJson);

			// Hack to add the missing block_height value into transactions
			foreach (var jToken in blockJObject["tx"]?.AsJEnumerable() ?? Enumerable.Empty<JToken>())
			{
				jToken["block_height"] = blockJObject["height"];
				jToken["double_spend"] = false;
			}

			return blockJObject.ToObject<Block>() ?? default;
		}

		/// <summary>
		/// Deserializes multiple blocks from the specified JSON string.
		/// </summary>
		/// <param name="blocksJson">The blocks JSON string.</param>
		/// <returns>System.Collections.ObjectModel.ReadOnlyCollection&lt;Info.Blockchain.API.Models.Block&gt;.</returns>
		public static new ReadOnlyCollection<Block> DeserializeMultiple(string blocksJson)
		{
			var blocksJObject = JObject.Parse(blocksJson);

			var blocks = blocksJObject["blocks"]
				?.AsJEnumerable()
				.Select(jToken => Deserialize(jToken.ToString()))
				.Where(b => b is not null)
				.OfType<Block>()
				.ToList()
				?? new List<Block>();

			return new ReadOnlyCollection<Block>(blocks);
		}
	}
}
