namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	/// <summary>
	/// The callback log class.
	/// </summary>
	public class CallbackLog
	{
		/// <summary>
		/// Gets the callback URL.
		/// </summary>
		/// <value>The callback URL.</value>
		[JsonProperty("callback")]
		[System.Text.Json.Serialization.JsonPropertyName("callback")]
		public string? CallbackUrl { get; init; }

		/// <summary>
		/// Gets the call date string.
		/// </summary>
		/// <value>The call date string.</value>
		[JsonProperty("called_at")]
		[System.Text.Json.Serialization.JsonPropertyName("called_at")]
		public string? CallDateString { get; init; }

		/// <summary>
		/// Gets the raw response.
		/// </summary>
		/// <value>The raw response.</value>
		[JsonProperty("raw_response")]
		[System.Text.Json.Serialization.JsonPropertyName("raw_response")]
		public string? RawResponse { get; init; }

		/// <summary>
		/// Gets the response code.
		/// </summary>
		/// <value>The response code.</value>
		[JsonProperty("response_code")]
		[System.Text.Json.Serialization.JsonPropertyName("response_code")]
		public int ResponseCode { get; init; }
	}
}
