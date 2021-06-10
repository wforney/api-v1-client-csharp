namespace Info.Blockchain.API.Client
{
	using Info.Blockchain.API.ExchangeRates;
	using Info.Blockchain.API.PushTx;
	using Info.Blockchain.API.Statistics;
	using Info.Blockchain.API.Wallet;

	using System;

	/// <summary>
	/// The blockchain API helper class. Implements the <see cref="IDisposable" />.
	/// </summary>
	/// <seealso cref="IDisposable" />
	public class BlockchainApiHelper : IDisposable
	{
		/// <summary>
		/// The block explorer
		/// </summary>
		public readonly BlockExplorer.BlockExplorer blockExplorer;

		/// <summary>
		/// The exchange rate explorer
		/// </summary>
		public readonly ExchangeRateExplorer exchangeRateExplorer;

		/// <summary>
		/// The statistics explorer
		/// </summary>
		public readonly StatisticsExplorer statisticsExplorer;

		/// <summary>
		/// The transaction broadcaster
		/// </summary>
		public readonly TransactionPusher transactionBroadcaster;

		/// <summary>
		/// The wallet creator
		/// </summary>
		public readonly WalletCreator? walletCreator;

		/// <summary>
		/// The base HTTP client
		/// </summary>
		private readonly IHttpClient? baseHttpClient;

		/// <summary>
		/// The service HTTP client
		/// </summary>
		private readonly IHttpClient? serviceHttpClient;

		/// <summary>
		/// The disposed value
		/// </summary>
		private bool disposedValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="BlockchainApiHelper" /> class.
		/// </summary>
		/// <param name="apiCode">The API code.</param>
		/// <param name="baseHttpClient">The base HTTP client.</param>
		/// <param name="serviceUrl">The service URL.</param>
		/// <param name="serviceHttpClient">The service HTTP client.</param>
		public BlockchainApiHelper(string? apiCode = null, IHttpClient? baseHttpClient = null, string? serviceUrl = null, IHttpClient? serviceHttpClient = null)
		{
			if (baseHttpClient is null)
			{
				baseHttpClient = new BlockchainHttpClient(apiCode);
			}
			else
			{
				this.baseHttpClient = baseHttpClient;
				if (apiCode is not null)
				{
					baseHttpClient.ApiCode = apiCode;
				}
			}

			if (serviceHttpClient is null && serviceUrl is not null)
			{
				serviceHttpClient = new BlockchainHttpClient(apiCode, serviceUrl);
			}
			else if (serviceHttpClient is not null)
			{
				this.serviceHttpClient = serviceHttpClient;
				if (apiCode is not null)
				{
					serviceHttpClient.ApiCode = apiCode;
				}
			}
			else
			{
				serviceHttpClient = null;
			}

			this.blockExplorer = new BlockExplorer.BlockExplorer(baseHttpClient);
			this.transactionBroadcaster = new TransactionPusher(baseHttpClient);
			this.exchangeRateExplorer = new ExchangeRateExplorer(baseHttpClient);
			this.statisticsExplorer = new StatisticsExplorer(new BlockchainHttpClient(uri: "https://api.blockchain.info"));

			this.walletCreator = serviceHttpClient is null ? null : new WalletCreator(serviceHttpClient);
		}

		/// <summary>
		/// Finalizes this instance.
		/// </summary>
		~BlockchainApiHelper()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: false);
		}

		/// <summary>
		/// Creates the wallet creator.
		/// </summary>
		/// <returns>Info.Blockchain.API.Wallet.WalletCreator.</returns>
		public WalletCreator CreateWalletCreator() => new(this.serviceHttpClient);

		/// <inheritdoc />
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Creates an instance of 'WalletHelper' based on the identifier allowing the use of that wallet
		/// </summary>
		/// <param name="identifier">Wallet identifier (GUID)</param>
		/// <param name="password">Decryption password</param>
		/// <param name="secondPassword">Second password</param>
		/// <exception cref="ClientApiException"></exception>
		public Wallet InitializeWallet(string identifier, string password, string? secondPassword = null)
		{
			return this.serviceHttpClient is null
				? throw new ClientApiException("In order to create wallets, you must provide a valid service_url to BlockchainApiHelper")
				: new Wallet(this.serviceHttpClient, identifier, password, secondPassword);
		}

		/// <inheritdoc />
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					// dispose managed state (managed objects)
					this.baseHttpClient?.Dispose();
					this.serviceHttpClient?.Dispose();
				}

				// free unmanaged resources (unmanaged objects) and override finalizer set large
				// fields to null
				this.disposedValue = true;
			}
		}
	}
}
