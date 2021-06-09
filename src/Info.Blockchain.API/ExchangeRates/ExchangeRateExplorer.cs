namespace Info.Blockchain.API.ExchangeRates
{
	using Info.Blockchain.API.Client;
	using Info.Blockchain.API.Models;

	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Threading.Tasks;

	/// <summary>
	/// This class reflects the functionality documented at
	/// https://blockchain.info/api/exchange_rates_api. It allows users to fetch the latest ticker
	/// data and convert amounts between BTC and fiat currencies.
	/// </summary>
	public class ExchangeRateExplorer : IDisposable
	{
		/// <summary>
		/// The HTTP client
		/// </summary>
		private readonly IHttpClient httpClient;

		/// <summary>
		/// The disposed value
		/// </summary>
		private bool disposedValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExchangeRateExplorer" /> class.
		/// </summary>
		public ExchangeRateExplorer() => this.httpClient = new BlockchainHttpClient();

		/// <summary>
		/// Initializes a new instance of the <see cref="ExchangeRateExplorer" /> class.
		/// </summary>
		/// <param name="httpClient">The HTTP client.</param>
		internal ExchangeRateExplorer(IHttpClient httpClient) => this.httpClient = httpClient;

		/// <summary>
		/// Finalizes an instance of the <see cref="ExchangeRateExplorer" /> class.
		/// </summary>
		~ExchangeRateExplorer()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: false);
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
		/// Converts a BitcoinValue object to its value in the specified currency
		/// </summary>
		/// <param name="btc">BitcoinValue representing the value to convert from</param>
		/// <param name="currency">Currency code (default USD)</param>
		/// <returns>Converted value in currency of choice</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public async Task<double> FromBtcAsync(BitcoinValue btc, string currency = "USD")
		{
			if (btc is null)
			{
				throw new ArgumentNullException(nameof(btc));
			}

			if (btc.GetBtc() <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(btc), "BitcoinValue must represent a value higher than 0");
			}

			var queryString = new QueryString();
			queryString.Add("currency", currency);
			queryString.Add("value", btc.Satoshis.ToString(CultureInfo.CurrentCulture));

			return await this.httpClient.GetAsync("frombtc", queryString, s => double.Parse(s, NumberStyles.Any)).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets the price ticker from https://blockchain.info/ticker
		/// </summary>
		/// <returns>
		/// A dictionary of currencies where the key is a 3-letter currency symbol and the value is
		/// the `Currency` class
		/// </returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<Dictionary<string, Currency>?> GetTickerAsync() => await this.httpClient.GetAsync<Dictionary<string, Currency>>("ticker").ConfigureAwait(false);

		/// <summary>
		/// Converts x value in the provided currency to BTC.
		/// </summary>
		/// <param name="currency">Currency code</param>
		/// <param name="value">Value to convert</param>
		/// <returns>Converted value in BTC</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public async Task<double> ToBtcAsync(string currency, double value)
		{
			if (string.IsNullOrWhiteSpace(currency))
			{
				throw new ArgumentNullException(nameof(currency));
			}

			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(value), "Value must be greater than 0");
			}

			var queryString = new QueryString();
			queryString.Add("currency", currency);
			queryString.Add("value", value.ToString(CultureInfo.CurrentCulture));

			return await this.httpClient.GetAsync("tobtc", queryString, s => double.Parse(s, NumberStyles.Any)).ConfigureAwait(false);
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
