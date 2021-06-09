namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// This class is used in the response of the `GetTicker` method in the `ExchangeRates` class.
	/// </summary>
	public class Currency
	{
		/// <summary>
		/// Current buy price
		/// </summary>
		[JsonProperty("buy", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("buy")]
		public double Buy { get; init; }

		/// <summary>
		/// Most recent market price
		/// </summary>
		[JsonProperty("last", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("last")]
		public double Last { get; init; }

		/// <summary>
		/// 15 minutes delayed market price
		/// </summary>
		[JsonProperty("15m", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("15m")]
		public double Price15M { get; init; }

		/// <summary>
		/// Current sell price
		/// </summary>
		[JsonProperty("sell", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("sell")]
		public double Sell { get; init; }

		/// <summary>
		/// Currency symbol
		/// </summary>
		[JsonProperty("symbol", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("symbol")]
		public string Symbol { get; init; } = string.Empty;
	}
}
