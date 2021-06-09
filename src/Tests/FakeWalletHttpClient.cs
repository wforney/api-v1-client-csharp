namespace Info.Blockchain.API.Tests
{
	using Info.Blockchain.API.Client;
	using Info.Blockchain.API.Models;

	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Threading.Tasks;

	/// <summary>
	/// The FakeWalletHttpClient class. Implements the <see cref="IHttpClient" />.
	/// </summary>
	/// <seealso cref="IHttpClient" />
	public class FakeWalletHttpClient : IHttpClient
	{
		/// <summary>
		/// The disposed value
		/// </summary>
		private bool disposedValue;

		/// <summary>
		/// Finalizes an instance of the <see cref="FakeWalletHttpClient" /> class.
		/// </summary>
		~FakeWalletHttpClient()
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
		/// Performs an HTTP GET.
		/// </summary>
		/// <typeparam name="T">The type of the response.</typeparam>
		/// <param name="route">The route.</param>
		/// <param name="queryString">The query string.</param>
		/// <param name="customDeserialization">The custom deserialization.</param>
		/// <returns>The result.</returns>
		/// <exception cref="NotImplementedException"></exception>
		[SuppressMessage("General", "RCS1079:Throwing of new NotImplementedException.", Justification = "Mock object for testing.")]
		public Task<T?> GetAsync<T>(string route, QueryString? queryString = null, Func<string, T>? customDeserialization = null) => throw new NotImplementedException();

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
		public Task<TResponse?> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse>? customDeserialization = null, bool multiPartContent = false, string? contentType = null)
		{
			var walletResponse = ReflectionUtil.DeserializeFile<CreateWalletResponse>("create_wallet_mock");
			return walletResponse is TResponse ? Task.FromResult((TResponse?)(object)walletResponse) : Task.FromResult(default(TResponse));
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
				}

				// free unmanaged resources (unmanaged objects) and override finalizer set large
				// fields to null
				this.disposedValue = true;
			}
		}
	}
}
