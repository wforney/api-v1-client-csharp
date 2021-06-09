namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	/// <summary>
	/// The create wallet request class.
	/// </summary>
	public class CreateWalletRequest
	{
		/// <summary>
		/// Gets or sets the API code.
		/// </summary>
		/// <value>The API code.</value>
		[JsonProperty("api_code")]
		[System.Text.Json.Serialization.JsonPropertyName("api_code")]
		public string? ApiCode { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		[JsonProperty("email")]
		[System.Text.Json.Serialization.JsonPropertyName("email")]
		public string? Email { get; set; }

		/// <summary>
		/// Gets or sets the label.
		/// </summary>
		/// <value>The label.</value>
		[JsonProperty("label")]
		[System.Text.Json.Serialization.JsonPropertyName("label")]
		public string? Label { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		[JsonProperty("password")]
		[System.Text.Json.Serialization.JsonPropertyName("password")]
		public string? Password { get; set; }

		/// <summary>
		/// Gets or sets the private key.
		/// </summary>
		/// <value>The private key.</value>
		[JsonProperty("privateKey")]
		[System.Text.Json.Serialization.JsonPropertyName("privateKey")]
		public string? PrivateKey { get; set; }
	}
}
