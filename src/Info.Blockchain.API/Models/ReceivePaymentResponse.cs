namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// The receive payment response class.
	/// </summary>
	public class ReceivePaymentResponse
	{
		/// <summary>
		/// The newly generated address
		/// </summary>
		[JsonProperty("address", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("address")]
		public string Address { get; init; } = string.Empty;

		/// <summary>
		/// The callback URL
		/// </summary>
		[JsonProperty("callback", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("callback")]
		public string Callback { get; init; } = string.Empty;

		/// <summary>
		/// The current index of the provided xpub
		/// </summary>
		[JsonProperty("index", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("index")]
		public int Index { get; init; }
	}
}
