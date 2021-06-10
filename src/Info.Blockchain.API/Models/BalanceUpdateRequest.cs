namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	/// <summary>
	/// The balance update request class.
	/// </summary>
	public class BalanceUpdateRequest
	{
		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		[JsonProperty("addr")]
		[System.Text.Json.Serialization.JsonPropertyName("addr")]
		public string? Address { get; set; }

		/// <summary>
		/// Gets or sets the callback.
		/// </summary>
		/// <value>The callback.</value>
		[JsonProperty("callback")]
		[System.Text.Json.Serialization.JsonPropertyName("callback")]
		public string? Callback { get; set; }

		/// <summary>
		/// Gets or sets the confirmations.
		/// </summary>
		/// <value>The confirmations.</value>
		[JsonProperty("confs")]
		[System.Text.Json.Serialization.JsonPropertyName("confs")]
		public int? Confirmations { get; set; }

		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>The key.</value>
		[JsonProperty("key")]
		[System.Text.Json.Serialization.JsonPropertyName("key")]
		public string? key { get; set; }

		/// <summary>
		/// Gets or sets the notification.
		/// </summary>
		/// <value>The notification.</value>
		[JsonProperty("onNotification")]
		[System.Text.Json.Serialization.JsonPropertyName("onNotification")]
		public string? Notification { get; set; }

		/// <summary>
		/// Gets or sets the type of the operation.
		/// </summary>
		/// <value>The type of the operation.</value>
		[JsonProperty("op")]
		[System.Text.Json.Serialization.JsonPropertyName("op")]
		public string? OperationType { get; set; }
	}
}
