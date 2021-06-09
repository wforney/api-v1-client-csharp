namespace Info.Blockchain.API.Wallet
{
	using Info.Blockchain.API.Client;
	using Info.Blockchain.API.Models;

	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// This class reflects the functionality documented at https://blockchain.info/api/create_wallet.
	/// </summary>
	public class WalletCreator : IDisposable
	{
		/// <summary>
		/// The HTTP client
		/// </summary>
		private readonly IHttpClient? httpClient;

		/// <summary>
		/// The disposed value
		/// </summary>
		private bool disposedValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="WalletCreator" /> class.
		/// </summary>
		public WalletCreator() => this.httpClient = new BlockchainHttpClient(uri: "http://127.0.0.1:3000");

		/// <summary>
		/// Initializes a new instance of the <see cref="WalletCreator" /> class.
		/// </summary>
		/// <param name="httpClient">The HTTP client.</param>
		public WalletCreator(IHttpClient? httpClient) => this.httpClient = httpClient;

		/// <summary>
		/// Finalizes an instance of the <see cref="WalletCreator" /> class.
		/// </summary>
		~WalletCreator()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: false);
		}

		/// <summary>
		/// Creates a new Blockchain.info wallet if the user's API code has the 'generate wallet'
		/// permission. It can be created containing a pre-generated private key or will otherwise
		/// generate a new private key.
		/// </summary>
		/// <param name="password">Password for the new wallet. At least 10 characters.</param>
		/// <param name="privateKey">Private key to add to the wallet</param>
		/// <param name="label">Label for the first address in the wallet</param>
		/// <param name="email">Email to associate with the new wallet</param>
		/// <returns>An instance of the CreateWalletResponse class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<CreateWalletResponse?> CreateAsync(string password, string? privateKey = null, string? label = null, string? email = null)
		{
			if (string.IsNullOrWhiteSpace(password))
			{
				throw new ArgumentNullException(nameof(password));
			}

			if (string.IsNullOrWhiteSpace(this.httpClient?.ApiCode))
			{
				throw new ArgumentNullException("Api code must be specified", innerException: null);
			}

			var request = new CreateWalletRequest
			{
				Password = password,
				ApiCode = this.httpClient.ApiCode,
				PrivateKey = privateKey,
				Label = label,
				Email = email
			};

			return await this.httpClient.PostAsync<CreateWalletRequest, CreateWalletResponse>("api/v2/create/", request, contentType: "application/json").ConfigureAwait(false);
		}

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
	}
}
