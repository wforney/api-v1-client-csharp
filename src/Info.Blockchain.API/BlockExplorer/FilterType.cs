namespace Info.Blockchain.API.BlockExplorer
{
	/// <summary>
	/// The filter type enumeration.
	/// </summary>
	public enum FilterType
	{
		/// <summary>
		/// All
		/// </summary>
		All = 4,

		/// <summary>
		/// The confirmed only
		/// </summary>
		ConfirmedOnly,

		/// <summary>
		/// The remove unspendable
		/// </summary>
		RemoveUnspendable
	}
}
