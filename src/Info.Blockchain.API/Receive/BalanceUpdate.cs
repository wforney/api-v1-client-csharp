namespace Info.Blockchain.API.Receive
{
	using Info.Blockchain.API.Client;
	using Info.Blockchain.API.Models;

	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// The balance update class.
	/// </summary>
	public class BalanceUpdate
	{
		/// <summary>
		/// The HTTP client
		/// </summary>
		private readonly IHttpClient httpClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="BalanceUpdate" /> class.
		/// </summary>
		/// <param name="httpClient">The HTTP client.</param>
		public BalanceUpdate(IHttpClient? httpClient = null)
		{
			this.httpClient = httpClient ?? new BlockchainHttpClient(uri: "https://api.blockchain.info/v2");
		}

		/// <summary>
		/// Subscribe to balance update notification whenever a transaction occur on the address
		/// </summary>
		/// <param name="key">Your blockchain.info receive payments v2 api key</param>
		/// <param name="address">The address you will like to monitor</param>
		/// <param name="callback">The callback URL to be notified when payment is made</param>
		/// <param name="notification">The request notification behaviour ('KEEP' | 'DELETE).</param>
		/// <param name="operationType">
		/// The operation type you would like to receive notifications for ('SPEND' | 'RECEIVE' | 'ALL').
		/// </param>
		/// <param name="confirmations">
		/// The number of confirmations the transaction needs to have before a notification is sent.
		/// </param>
		/// <returns>The balance update response.</returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<BalanceUpdateResponse?> Subscribe(string key, string address, string callback, string notification = "KEEP", string operationType = "ALL", int confirmations = 3)
		{
			try
			{
				var request = new BalanceUpdateRequest
				{
					key = key,
					Address = address,
					Callback = callback,
					Confirmations = confirmations,
					Notification = notification,
					OperationType = operationType
				};

				return await httpClient.PostAsync<BalanceUpdateRequest, BalanceUpdateResponse>("balance_update", request).ConfigureAwait(false);
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
