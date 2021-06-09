namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	/// <summary>
	/// Represents a multiaddr response
	/// </summary>
	public class MultiAddress
	{
		/// <summary>
		/// Gets the addresses.
		/// </summary>
		/// <value>The addresses.</value>
		[JsonProperty("addresses", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("addresses")]
		public IEnumerable<Address> Addresses { get; init; } = Enumerable.Empty<Address>();

		/// <summary>
		/// List of up to 50 transactions associated with the specified address
		/// </summary>
		[JsonProperty("txs", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("txs")]
		public IEnumerable<Transaction> Transactions { get; init; } = Enumerable.Empty<Transaction>();
	}
}
