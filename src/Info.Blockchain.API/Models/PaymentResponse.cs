namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Used as a response object to the `send` and `sendMany` methods in the `Wallet` class.
	/// </summary>
	public class PaymentResponse
	{
		/// <summary>
		/// Response message from the server
		/// </summary>
		[JsonProperty("message", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("message")]
		public string Message { get; init; } = string.Empty;

		/// <summary>
		/// Additional response message from the server
		/// </summary>
		[JsonProperty("notice", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("notice")]
		public string Notice { get; init; } = string.Empty;

		/// <summary>
		/// Transaction hash
		/// </summary>
		[JsonProperty("tx_hash", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("tx_hash")]
		public string TxHash { get; init; } = string.Empty;
	}
}
