namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Used as a response to the `GetLatestBlock` method in the `BlockExplorer` class.
	/// </summary>
	public sealed class LatestBlock : SimpleBlock
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="LatestBlock"/> class from being created.
		/// </summary>
		[JsonConstructor]
		[System.Text.Json.Serialization.JsonConstructor]
		public LatestBlock() : base(true)
		{
		}

		/// <summary>
		/// Block index
		/// </summary>
		[JsonProperty("block_index", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("block_index")]
		public long Index { get; init; }

		/// <summary>
		/// Transaction indexes included in this block
		/// </summary>
		[JsonProperty("txIndexes", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("txIndexes")]
		public IReadOnlyCollection<long> TransactionIndexes { get; init; } = new ReadOnlyCollection<long>(new List<long>());
	}
}
