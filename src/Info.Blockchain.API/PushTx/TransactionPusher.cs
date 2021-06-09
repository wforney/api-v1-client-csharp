namespace Info.Blockchain.API.PushTx
{
	using Info.Blockchain.API.Client;

	using System;
	using System.Threading.Tasks;

	/// <summary>
	/// This class reflects the functionality provided at https://blockchain.info/pushtx. It allows
	/// users to broadcast hex encoded transactions to the bitcoin network.
	/// </summary>
	public class TransactionPusher
	{
		/// <summary>
		/// The HTTP client
		/// </summary>
		private readonly IHttpClient httpClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="TransactionPusher" /> class.
		/// </summary>
		public TransactionPusher() => this.httpClient = new BlockchainHttpClient();

		/// <summary>
		/// Initializes a new instance of the <see cref="TransactionPusher" /> class.
		/// </summary>
		/// <param name="httpClient">The HTTP client.</param>
		public TransactionPusher(IHttpClient httpClient) => this.httpClient = httpClient;

		/// <summary>
		/// Pushes a hex encoded transaction to the network.
		/// </summary>
		/// <param name="transactionString">Hex encoded transaction</param>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task PushTransactionAsync(string transactionString)
		{
			if (string.IsNullOrWhiteSpace(transactionString))
			{
				throw new ArgumentNullException(nameof(transactionString));
			}

			_ = await this.httpClient.PostAsync<string, object>("pushtx", transactionString, multiPartContent: true).ConfigureAwait(false);
		}
	}
}
