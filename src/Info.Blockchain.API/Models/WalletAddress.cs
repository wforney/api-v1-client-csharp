namespace Info.Blockchain.API.Models
{
	using Info.Blockchain.API.Json;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Used in combination with the `Wallet` class
	/// </summary>
	public class WalletAddress
	{
		/// <summary>
		/// String representation of the address
		/// </summary>
		[JsonProperty("address", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("address")]
		public string AddressStr { get; init; } = string.Empty;

		/// <summary>
		/// Balance in bitcoins
		/// </summary>
		[JsonProperty("balance")]
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("balance")]
		public BitcoinValue Balance { get; init; } = BitcoinValue.Zero;

		/// <summary>
		/// Label attached to the address
		/// </summary>
		[JsonProperty("label")]
		[System.Text.Json.Serialization.JsonPropertyName("label")]
		public string? Label { get; init; }

		/// <summary>
		/// Total received amount
		/// </summary>
		[JsonConverter(typeof(BitcoinValueJsonConverter))]
		[JsonProperty("total_received")]
		[System.Text.Json.Serialization.JsonConverter(typeof(NativeBitcoinValueJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("total_received")]
		public BitcoinValue TotalReceived { get; init; } = new(0);

		/// <summary>
		/// Deserializes the archived property of the specified JSON.
		/// </summary>
		/// <param name="json">The JSON string.</param>
		/// <returns>string?.</returns>
		public static string? DeserializeArchived(string json)
		{
			JObject jObject = JObject.Parse(json);
			return jObject["archived"]?.ToObject<string>();
		}

		/// <summary>
		/// Deserializes the consolidated property of the specified JSON.
		/// </summary>
		/// <param name="json">The JSON string.</param>
		/// <returns>System.Collections.Generic.List&lt;string?&gt;.</returns>
		public static List<string?> DeserializeConsolidated(string json)
		{
			JObject jObject = JObject.Parse(json);
			return jObject["consolidated"]?.ToObject<List<string?>>() ?? new List<string?>();
		}

		/// <summary>
		/// Deserializes multiple wallet addresses from the specified JSON string.
		/// </summary>
		/// <param name="json">The JSON string.</param>
		/// <returns>System.Collections.Generic.List&lt;Info.Blockchain.API.Models.WalletAddress?&gt;.</returns>
		public static List<WalletAddress?> DeserializeMultiple(string json)
		{
			JObject jObject = JObject.Parse(json);
			return jObject["addresses"]?.ToObject<List<WalletAddress?>>() ?? new List<WalletAddress?>();
		}

		/// <summary>
		/// Deserializes the active property of the specified JSON.
		/// </summary>
		/// <param name="json">The JSON string.</param>
		/// <returns>string?.</returns>
		public static string? DeserializeUnArchived(string json)
		{
			JObject jObject = JObject.Parse(json);
			return jObject["active"]?.ToObject<string>();
		}
	}
}
