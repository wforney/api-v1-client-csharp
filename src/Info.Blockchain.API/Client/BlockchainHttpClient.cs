namespace Info.Blockchain.API.Client
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using System.Net;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// The blockchain HTTP client class. Implements the <see cref="IHttpClient" />.
	/// </summary>
	/// <seealso cref="IHttpClient" />
	public class BlockchainHttpClient : IHttpClient
	{
		/// <summary>
		/// The base URI
		/// </summary>
		private const string BASE_URI = "https://blockchain.info";

		/// <summary>
		/// The timeout ms
		/// </summary>
		private const int TIMEOUT_MS = 100000;

		/// <summary>
		/// The HTTP client
		/// </summary>
		private readonly HttpClient httpClient;

		/// <summary>
		/// The disposed value
		/// </summary>
		private bool disposedValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="BlockchainHttpClient" /> class.
		/// </summary>
		/// <param name="apiCode">The API code.</param>
		/// <param name="uri">The URI.</param>
		public BlockchainHttpClient(string? apiCode = null, string uri = BASE_URI)
		{
			this.ApiCode = apiCode;
			this.httpClient = new HttpClient
			{
				BaseAddress = new Uri(uri),
				Timeout = TimeSpan.FromMilliseconds(TIMEOUT_MS)
			};
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="BlockchainHttpClient" /> class.
		/// </summary>
		~BlockchainHttpClient()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: false);
		}

		/// <summary>
		/// Gets or sets the API code.
		/// </summary>
		/// <value>The API code.</value>
		public string? ApiCode { get; set; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting
		/// unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// get as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="route">The route.</param>
		/// <param name="queryString">The query string.</param>
		/// <param name="customDeserialization">The custom deserialization.</param>
		/// <returns>T.</returns>
		/// <exception cref="ArgumentNullException">route</exception>
		[SuppressMessage("Usage", "SecurityIntelliSenseCS:MS Security rules violation", Justification = "<Pending>")]
		public async Task<T?> GetAsync<T>(string route, QueryString? queryString = null, Func<string, T>? customDeserialization = null)
		{
			if (route is null)
			{
				throw new ArgumentNullException(nameof(route));
			}

			if (this.ApiCode is not null)
			{
				queryString?.Add("api_code", this.ApiCode);
			}

			if (queryString?.Count > 0)
			{
				var queryStringIndex = route.IndexOf('?');
				if (queryStringIndex >= 0)
				{
					// Append to querystring
					var queryStringValue = queryStringIndex.ToString(CultureInfo.CurrentCulture);

					// replace questionmark with &
					queryStringValue = $"&{queryStringValue[1..]}";
					route += queryStringValue;
				}
				else
				{
					route += queryString.ToString();
				}
			}

			var response = await this.httpClient.GetAsync(route).ConfigureAwait(false);
			var responseString = await ValidateResponse(response).ConfigureAwait(false);
			if (responseString is null)
			{
				return default;
			}

			return customDeserialization is null
				? System.Text.Json.JsonSerializer.Deserialize<T>(responseString, new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web) { AllowTrailingCommas = true })
				: customDeserialization(responseString);
		}

		/// <summary>
		/// post as an asynchronous operation.
		/// </summary>
		/// <typeparam name="TPost">The type of the t post.</typeparam>
		/// <typeparam name="TResponse">The type of the t response.</typeparam>
		/// <param name="route">The route.</param>
		/// <param name="postObject">The post object.</param>
		/// <param name="customDeserialization">The custom deserialization.</param>
		/// <param name="multiPartContent">if set to <c>true</c> [multi part content].</param>
		/// <param name="contentType">Type of the content.</param>
		/// <returns>TResponse.</returns>
		/// <exception cref="ArgumentNullException">route</exception>
		[SuppressMessage("Usage", "SecurityIntelliSenseCS:MS Security rules violation", Justification = "<Pending>")]
		public async Task<TResponse?> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse>? customDeserialization = null, bool multiPartContent = false, string? contentType = "application/x-www-form-urlencoded")
		{
			if (route is null)
			{
				throw new ArgumentNullException(nameof(route));
			}

			if (this.ApiCode is not null)
			{
				route += $"?api_code={this.ApiCode}";
			}

			var json = System.Text.Json.JsonSerializer.Serialize(postObject);
			var httpContent = multiPartContent
				? new MultipartFormDataContent { new StringContent(json, Encoding.UTF8, contentType) }
				: (HttpContent)new StringContent(json, Encoding.UTF8, contentType);
			var response = await this.httpClient.PostAsync(route, httpContent).ConfigureAwait(false);
			var responseString = await ValidateResponse(response).ConfigureAwait(false);
			if (responseString is null)
			{
				return default;
			}

			return System.Text.Json.JsonSerializer.Deserialize<TResponse>(responseString, new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web) { AllowTrailingCommas = true });
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing">
		/// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
		/// only unmanaged resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					// dispose managed state (managed objects)
					this.httpClient?.Dispose();
				}

				// free unmanaged resources (unmanaged objects) and override finalizer set large
				// fields to null
				this.disposedValue = true;
			}
		}

		/// <summary>
		/// Validates the response.
		/// </summary>
		/// <param name="response">The response.</param>
		/// <returns>The input response.</returns>
		/// <exception cref="ServerApiException">Block Not Found or</exception>
		private static async Task<string?> ValidateResponse(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode)
			{
				var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				if (responseString?.StartsWith("{\"error\":", StringComparison.Ordinal) == true)
				{
					var jObject = JObject.Parse(responseString);
					var message = jObject["error"]?.ToObject<string>();
					throw new ServerApiException(message ?? string.Empty, HttpStatusCode.BadRequest);
				}

				return responseString;
			}

			var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			if (string.Equals(responseContent, "Block Not Found", StringComparison.OrdinalIgnoreCase))
			{
				throw new ServerApiException("Block Not Found", HttpStatusCode.NotFound);
			}

			throw new ServerApiException($"{response.ReasonPhrase}: {responseContent}", response.StatusCode);
		}
	}
}
