namespace Info.Blockchain.API.Models
{
	using Newtonsoft.Json;

	using System.ComponentModel.DataAnnotations;

	/// <summary>
	/// Represents a transaction input. If the PreviousOutput object is null, this is a coinbase input.
	/// </summary>
	public sealed class Input
	{
		/// <summary>
		/// Prevents a default instance of the <see cref="Input" /> class from being created.
		/// </summary>
		[JsonConstructor]
		[System.Text.Json.Serialization.JsonConstructor]
		public Input()
		{
		}

		/// <summary>
		/// Previous output. If null, this is a coinbase input.
		/// </summary>
		[JsonProperty("prev_out")]
		[System.Text.Json.Serialization.JsonPropertyName("prev_out")]
		public Output? PreviousOutput { get; init; }

		/// <summary>
		/// Script signature
		/// </summary>
		[JsonProperty("script", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("script")]
		public string ScriptSignature { get; init; } = string.Empty;

		/// <summary>
		/// Sequence number of the input
		/// </summary>
		[JsonProperty("sequence", Required = Required.Always)]
		[Required]
		[System.Text.Json.Serialization.JsonPropertyName("sequence")]
		public long Sequence { get; init; }
	}
}
