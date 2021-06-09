namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;

	/// <summary>
	/// This class is used as a response object to the chart 'get' method in the 'Statistics' class
	/// </summary>
	public sealed class ChartResponse
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="ChartResponse"/> class from being created.
		/// </summary>
		[JsonConstructor]
		[System.Text.Json.Serialization.JsonConstructor]
		public ChartResponse()
		{
		}

		/// <summary>
		/// Chart name
		/// </summary>
		[JsonProperty("name", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("name")]
		public string? ChartName { get; init; }

		/// <summary>
		/// A description of the chart
		/// </summary>
		[JsonProperty("description", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("description")]
		public string? Description { get; init; }

		/// <summary>
		/// The timespan covered in this chart response
		/// </summary>
		[JsonProperty("period", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("period")]
		public string? Timespan { get; init; }

		/// <summary>
		/// Measuring unit
		/// </summary>
		[JsonProperty("unit", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("unit")]
		public string? Unit { get; init; }

		/// <summary>
		/// Chart values
		/// </summary>
		[JsonProperty("values", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("values")]
		public IEnumerable<ChartValue> Values { get; init; } = Enumerable.Empty<ChartValue>();
	}
}
