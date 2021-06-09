namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// A class representing a single chart value
	/// </summary>
	public class ChartValue
	{
		/// <summary>
		/// X Value
		/// </summary>
		[JsonProperty("x", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("x")]
		public double X { get; init; }

		/// <summary>
		/// Y Value
		/// </summary>
		[JsonProperty("y", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("y")]
		public double Y { get; init; }
	}
}
