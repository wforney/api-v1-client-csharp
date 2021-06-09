namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	/// <summary>
	/// The Xpub gap class.
	/// </summary>
	public class XpubGap
	{
		/// <summary>
		/// Gets the gap.
		/// </summary>
		/// <value>The gap.</value>
		[JsonProperty("gap")]
		[System.Text.Json.Serialization.JsonPropertyName("gap")]
		public int Gap { get; init; }
	}
}
