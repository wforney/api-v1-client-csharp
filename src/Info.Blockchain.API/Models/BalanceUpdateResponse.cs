namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// The balance update response class.
	/// </summary>
	public class BalanceUpdateResponse
	{
		/// <summary>
		/// The address you will like to monitor
		/// </summary>
		[JsonProperty("addr", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("addr")]
		public string Address { get; set; } = string.Empty;

		/// <summary>
		/// The callback URL to be notified when payment is made
		/// </summary>
		[JsonProperty("callback", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("callback")]
		public string Callback { get; set; } = string.Empty;

		/// <summary>
		/// The number of confirmations the transaction needs to have before a notification is sent.
		/// </summary>
		[JsonProperty("confs", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("confs")]
		public int Confirmations { get; set; }

		/// <summary>
		/// The id in the response can be used to delete
		/// </summary>
		[JsonProperty("id", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("id")]
		public int id { get; set; }

		/// <summary>
		/// The request notification behaviour ('KEEP' | 'DELETE).
		/// </summary>
		[JsonProperty("onNotification", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("onNotification")]
		public string Notification { get; set; } = string.Empty;

		/// <summary>
		/// The operation type you would like to receive notifications for ('SPEND' | 'RECEIVE' | 'ALL').
		/// </summary>
		[JsonProperty("op", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("op")]
		public string OperationType { get; set; } = string.Empty;
	}
}
