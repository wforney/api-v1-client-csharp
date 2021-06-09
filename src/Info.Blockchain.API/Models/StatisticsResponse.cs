namespace Info.Blockchain.API.Models
{
	using Info.Blockchain.API.Json;

	using Newtonsoft.Json;

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// This class is used as a response object to the 'get' method in the 'Statistics' class
	/// </summary>
	public class StatisticsResponse
	{
		/// <summary>
		/// Gets or sets the size of the blocks.
		/// </summary>
		/// <value>The size of the blocks.</value>
		[JsonProperty("blocks_size", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("blocks_size")]
		public long BlocksSize { get; init; }

		/// <summary>
		/// Number of BTC mined in the past 24 hours
		/// </summary>
		[JsonProperty("n_btc_mined", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("n_btc_mined")]
		public BitcoinValue BtcMined { get; init; } = new(0);

		/// <summary>
		/// Current difficulty
		/// </summary>
		[JsonProperty("difficulty", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("difficulty")]
		public double Difficulty { get; init; }

		/// <summary>
		/// Estimated BTC sent in the past 24 hours
		/// </summary>
		[JsonProperty("estimated_btc_sent", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("estimated_btc_sent")]
		public BitcoinValue EstimatedBtcSent { get; init; } = new(0);

		/// <summary>
		/// Estimated transaction volume in the past 24 hours
		/// </summary>
		[JsonProperty("estimated_transaction_volume_usd", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("estimated_transaction_volume_usd")]
		public double EstimatedTransactionVolumeUsd { get; init; }

		/// <summary>
		/// Current hashrate in GH/s
		/// </summary>
		[JsonProperty("hash_rate", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("hash_rate")]
		public double HashRate { get; set; }

		/// <summary>
		/// Current market price in USD
		/// </summary>
		[JsonProperty("market_price_usd", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("market_price_usd")]
		public double MarketPriceUsd { get; init; }

		/// <summary>
		/// Number of blocks mined in the past 24 hours
		/// </summary>
		[JsonProperty("n_blocks_mined", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("n_blocks_mined")]
		public long MinedBlocks { get; init; }

		/// <summary>
		/// Miners' revenue in BTC
		/// </summary>
		[JsonProperty("miners_revenue_btc", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("miners_revenue_btc")]
		public double MinersRevenueBtc { get; init; }

		/// <summary>
		/// Miners' revenue in USD
		/// </summary>
		[JsonProperty("miners_revenue_usd", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("miners_revenue_usd")]
		public double MinersRevenueUsd { get; init; }

		/// <summary>
		/// Minutes between blocks
		/// </summary>
		[JsonProperty("minutes_between_blocks", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("minutes_between_blocks")]
		public double MinutesBetweenBlocks { get; init; }

		/// <summary>
		/// The next block height where the difficulty retarget will occur
		/// </summary>
		[JsonProperty("nextretarget", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("nextretarget")]
		public long NextRetarget { get; init; }

		/// <summary>
		/// Number of transactions in the past 24 hours
		/// </summary>
		[JsonProperty("n_tx", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("n_tx")]
		public long NumberOfTransactions { get; init; }

		/// <summary>
		/// Timestamp of when this report was compiled (in ms)
		/// </summary>
		[JsonProperty("timestamp", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("timestamp")]
		public decimal Timestamp { get; init; }

		/// <summary>
		/// Total number of blocks in existence
		/// </summary>
		[JsonProperty("n_blocks_total", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("n_blocks_total")]
		public long TotalBlocks { get; init; }

		/// <summary>
		/// Total BTC in existence
		/// </summary>
		[JsonProperty("totalbc", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("totalbc")]
		public BitcoinValue TotalBtc { get; init; } = new(0);

		/// <summary>
		/// Total BTC sent in the past 24 hours
		/// </summary>
		[JsonProperty("total_btc_sent", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("total_btc_sent")]
		public BitcoinValue TotalBtcSent { get; init; } = new(0);

		/// <summary>
		/// Total fees in the past 24 hours
		/// </summary>
		[JsonProperty("total_fees_btc", Required = Required.Always)]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[Required]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("total_fees_btc")]
		public BitcoinValue TotalFeesBtc { get; init; } = new(0);

		/// <summary>
		/// Trade volume in the past 24 hours (in BTC)
		/// </summary>
		[JsonProperty("trade_volume_btc", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("trade_volume_btc")]
		public double TradeVolumeBtc { get; init; }

		/// <summary>
		/// Trade volume in the past 24 hours (in USD)
		/// </summary>
		[JsonProperty("trade_volume_usd", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("trade_volume_usd")]
		public double TradeVolumeUsd { get; init; }
	}
}
