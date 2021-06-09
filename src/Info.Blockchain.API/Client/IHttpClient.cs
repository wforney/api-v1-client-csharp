namespace Info.Blockchain.API.Client
{
	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// The HTTP client interface. Implements the <see cref="IDisposable" />.
	/// </summary>
	/// <seealso cref="IDisposable" />
	public interface IHttpClient : IDisposable
	{
		/// <summary>
		/// Gets or sets the API code.
		/// </summary>
		/// <value>The API code.</value>
		string? ApiCode { get; set; }

		/// <summary>
		/// Performs an HTTP GET.
		/// </summary>
		/// <typeparam name="T">The type of the response.</typeparam>
		/// <param name="route">The route.</param>
		/// <param name="queryString">The query string.</param>
		/// <param name="customDeserialization">The custom deserialization.</param>
		/// <returns>The result.</returns>
		Task<T?> GetAsync<T>(string route, QueryString? queryString = null, Func<string, T>? customDeserialization = null);

		/// <summary>
		/// Performs an HTTP POST.
		/// </summary>
		/// <typeparam name="TPost">The type of the post body.</typeparam>
		/// <typeparam name="TResponse">The type of the response.</typeparam>
		/// <param name="route">The route.</param>
		/// <param name="postObject">The post object.</param>
		/// <param name="customDeserialization">The custom deserialization.</param>
		/// <param name="multiPartContent">if set to <c>true</c> multi-part content.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <returns>The response.</returns>
		Task<TResponse?> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse>? customDeserialization = null, bool multiPartContent = false, string? contentType = null);
	}
}
