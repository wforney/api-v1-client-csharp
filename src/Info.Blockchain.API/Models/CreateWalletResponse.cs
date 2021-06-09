namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// The create wallet response class.
	/// </summary>
	public class CreateWalletResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CreateWalletResponse" /> class.
		/// </summary>
		[JsonConstructor]
		[System.Text.Json.Serialization.JsonConstructor]
		public CreateWalletResponse()
		{
		}

		/// <summary>
		/// First address in the wallet
		/// </summary>
		[JsonProperty("address", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("address")]
		public string Address { get; init; } = string.Empty;

		/// <summary>
		/// Wallet identifier (GUID)
		/// </summary>
		[JsonProperty("guid", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("guid")]
		public string Identifier { get; init; } = string.Empty;

		/// <summary>
		/// Optional label
		/// </summary>
		[JsonProperty("label")]
		[System.Text.Json.Serialization.JsonPropertyName("label")]
		public string? Label { get; init; }
	}
}
