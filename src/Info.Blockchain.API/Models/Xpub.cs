namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using System.Linq;

	/// <summary>
	/// The Xpub class. Implements the <see cref="Address" />.
	/// </summary>
	/// <seealso cref="Address" />
	public class Xpub : Address
	{
		/// <summary>
		/// Gets or sets the index of the account.
		/// </summary>
		/// <value>The index of the account.</value>
		[JsonProperty("account_index")]
		[System.Text.Json.Serialization.JsonPropertyName("account_index")]
		public int AccountIndex { get; init; }

		/// <summary>
		/// Gets or sets the index of the change.
		/// </summary>
		/// <value>The index of the change.</value>
		[JsonProperty("change_index")]
		[System.Text.Json.Serialization.JsonPropertyName("change_index")]
		public int ChangeIndex { get; init; }

		/// <summary>
		/// Gets or sets the gap limit.
		/// </summary>
		/// <value>The gap limit.</value>
		[JsonProperty("gap_limit")]
		[System.Text.Json.Serialization.JsonPropertyName("gap_limit")]
		public int GapLimit { get; init; }

		/// <summary>
		/// Deserializes the specified xpub json.
		/// </summary>
		/// <param name="xpubJson">The xpub json.</param>
		/// <returns>Info.Blockchain.API.Models.Xpub.</returns>
		public static Xpub? Deserialize(string xpubJson)
		{
			var xpubJObject = JObject.Parse(xpubJson);
			var xpubOutput = xpubJObject["addresses"]?.AsJEnumerable().FirstOrDefault() ?? new JObject();
			xpubOutput["txs"] = xpubJObject["txs"];
			return xpubOutput.ToObject<Xpub>();
		}
	}
}
