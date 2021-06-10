namespace Info.Blockchain.API.Receive
{
	using Info.Blockchain.API.Client;
	using Info.Blockchain.API.Models;

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// The receive class.
	/// </summary>
	public class Receive
	{
		/// <summary>
		/// The HTTP client
		/// </summary>
		private readonly IHttpClient httpClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="Receive" /> class.
		/// </summary>
		/// <param name="httpClient">The HTTP client.</param>
		public Receive(IHttpClient? httpClient = null) => this.httpClient = httpClient ?? new BlockchainHttpClient(uri: "https://api.blockchain.info/v2");

		/// <summary>
		/// Check the index gap between last address paid to and the last address generated using
		/// the checkgap endpoint
		/// </summary>
		/// <param name="xpub">The Xpub to check</param>
		/// <param name="key">The blockchain.info receive payments v2 api key</param>
		/// <returns>XpubGap object</returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<XpubGap?> CheckAddressGapAsync(string xpub, string key)
		{
			var queryString = new QueryString();
			queryString.Add("xpub", xpub);
			queryString.Add("key", key);

			try
			{
				return await this.httpClient.GetAsync<XpubGap>("receive/checkgap", queryString).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("Invalid xpub format"))
				{
					throw new ArgumentException("the xpub provided is invalid", nameof(xpub));
				}

				if (ex.Message.Contains("API Key is not valid"))
				{
					throw new ArgumentException("the api key provided is invalid", nameof(key));
				}

				throw;
			}
		}

		/// <summary>
		/// Generate a new address for an Xpub
		/// </summary>
		/// <param name="xpub">The Xpub to generate a new address from</param>
		/// <param name="callback">The callback URL to be notified when a payment is received</param>
		/// <param name="key">The blockchain.info receive payments v2 api key</param>
		/// <param name="gapLimit">
		/// Optional. How many unused addresses are allowed before erroring out
		/// </param>
		/// <returns>ReceivePaymentResponse object</returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<ReceivePaymentResponse?> GenerateAddressAsync(string xpub, string callback, string key, int? gapLimit)
		{
			var queryString = new QueryString();
			queryString.Add("xpub", xpub);
			queryString.Add("callback", callback);
			queryString.Add("key", key);
			if (gapLimit.HasValue)
			{
				queryString.Add("gap_limit", gapLimit.ToString() ?? string.Empty);
			}

			try
			{
				return await this.httpClient.GetAsync<ReceivePaymentResponse>("receive", queryString).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("Invalid xpub format"))
				{
					throw new ArgumentException("the xpub provided is invalid", nameof(xpub));
				}

				if (ex.Message.Contains("API Key is not valid"))
				{
					throw new ArgumentException("the api key provided is invalid", nameof(key));
				}

				throw;
			}
		}

		/// <summary>
		/// Get logs related to callback attempts
		/// </summary>
		/// <param name="callback">The callback url to check</param>
		/// <param name="key">The blockchain.info receive payments v2 api key</param>
		/// <returns>Logs related to the callback url supplied</returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<IEnumerable<CallbackLog>?> GetCallbackLogsAsync(string callback, string key)
		{
			var queryString = new QueryString();
			queryString.Add("callback", callback);
			queryString.Add("key", key);

			try
			{
				return await this.httpClient.GetAsync<IEnumerable<CallbackLog>>("receive/callback_log", queryString).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("API Key is not valid"))
				{
					throw new ArgumentException("the api key provided is invalid", nameof(key));
				}

				throw;
			}
		}
	}
}
