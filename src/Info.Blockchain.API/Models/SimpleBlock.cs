namespace Info.Blockchain.API.Models
{
	using Info.Blockchain.API.Json;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Simple representation of a block
	/// </summary>
	public class SimpleBlock
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SimpleBlock" /> class.
		/// </summary>
		/// <param name="mainChain">if set to <c>true</c> [main chain].</param>
		protected SimpleBlock(bool mainChain = false) => this.MainChain = mainChain;

		/// <summary>
		/// Prevents a default instance of the <see cref="SimpleBlock" /> class from being created.
		/// </summary>
		[JsonConstructor]
		[System.Text.Json.Serialization.JsonConstructor]
		public SimpleBlock()
		{
		}

		/// <summary>
		/// Block hash
		/// </summary>
		[JsonProperty("hash", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("hash")]
		public string Hash { get; init; } = string.Empty;

		/// <summary>
		/// Block height
		/// </summary>
		[JsonProperty("height", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("height")]
		public long Height { get; init; }

		/// <summary>
		/// Whether the block is on the main chain
		/// </summary>
		[JsonConverter(typeof(TrueTrumpsAllJsonConverter))]
		[JsonProperty("main_chain")]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeTrueTrumpsAllJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("main_chain")]
		public bool MainChain { get; init; }

		/// <summary>
		/// Block timestamp set by the miner
		/// </summary>
		[JsonConverter(typeof(UnixDateTimeJsonConverter))]
		[JsonProperty("time", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(UnixEpochDateTimeConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("time")]
		public DateTime Time { get; init; }

		/// <summary>
		/// Deserializes the multiple.
		/// </summary>
		/// <param name="blocksJson">The blocks json.</param>
		/// <returns>System.Collections.ObjectModel.ReadOnlyCollection&lt;Info.Blockchain.API.Models.SimpleBlock&gt;?.</returns>
		public static ReadOnlyCollection<SimpleBlock>? DeserializeMultiple(string blocksJson)
		{
			JObject blocksJObject = JObject.Parse(blocksJson);
			return blocksJObject["blocks"]?.ToObject<ReadOnlyCollection<SimpleBlock>>();
		}
	}
}
