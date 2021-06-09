namespace Info.Blockchain.API.Client
{
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// The query string class.
	/// </summary>
	public class QueryString
	{
		/// <summary>
		/// The query string
		/// </summary>
		private readonly IDictionary<string, string> queryString;

		/// <summary>
		/// Initializes a new instance of the <see cref="QueryString" /> class.
		/// </summary>
		public QueryString() => this.queryString = new Dictionary<string, string>();

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count => this.queryString.Count;

		/// <summary>
		/// Adds the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		/// <exception cref="ClientApiException">Query string already has a value for {key}</exception>
		public void Add(string key, string value)
		{
			if (this.queryString.ContainsKey(key))
			{
				throw new ClientApiException($"Query string already has a value for {key}");
			}

			this.queryString[key] = value;
		}

		/// <summary>
		/// Adds the or update.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void AddOrUpdate(string key, string value) => this.queryString[key] = value;

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="string" /> that represents this instance.</returns>
		public override string ToString() => $"?{string.Join("&", this.queryString.Select(kv => $"{kv.Key}={kv.Value}"))}";
	}
}
