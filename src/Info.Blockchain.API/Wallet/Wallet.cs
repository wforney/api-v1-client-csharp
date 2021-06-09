namespace Info.Blockchain.API.Wallet
{
	using Info.Blockchain.API.Client;
	using Info.Blockchain.API.Json;
	using Info.Blockchain.API.Models;

	using Newtonsoft.Json;

	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Threading.Tasks;

	/// <summary>
	/// This class reflects the functionality documented at
	/// https://blockchain.info/api/blockchain_wallet_api. It allows users to interact with their
	/// Blockchain.info wallet.
	/// </summary>
	public class Wallet
	{
		private readonly IHttpClient httpClient;
		private readonly string identifier;
		private readonly string password;
		private readonly string? secondPassword;

		/// <summary>
		/// Initializes a new instance of the <see cref="Wallet" /> class.
		/// </summary>
		/// <param name="httpClient">IHttpClient to access Blockchain REST API.</param>
		/// <param name="identifier">Wallet identifier (GUID).</param>
		/// <param name="password">Decryption password.</param>
		/// <param name="secondPassword">Second password.</param>
		internal Wallet(IHttpClient httpClient, string identifier, string password, string? secondPassword = null)
		{
			this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			this.identifier = identifier;
			this.password = password;
			this.secondPassword = secondPassword;
		}

		/// <summary>
		/// Archives an address.
		/// </summary>
		/// <param name="address">Address to archive</param>
		/// <returns>String representation of the archived address</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<string?> ArchiveAddressAsync(string address)
		{
			if (string.IsNullOrWhiteSpace(address))
			{
				throw new ArgumentNullException(nameof(address));
			}

			var queryString = this.BuildBasicQueryString();
			queryString.Add("address", address);

			var route = $"merchant/{this.identifier}/archive_address";
			return await this.httpClient.GetAsync(route, queryString, WalletAddress.DeserializeArchived).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieves an address from the wallet.
		/// </summary>
		/// <param name="address">Address in the wallet to look up</param>
		/// <returns>An instance of the Address class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<WalletAddress?> GetAddressAsync(string address)
		{
			if (string.IsNullOrWhiteSpace(address))
			{
				throw new ArgumentNullException(nameof(address));
			}

			var queryString = this.BuildBasicQueryString();
			queryString.Add("address", address);

			var route = $"merchant/{this.identifier}/address_balance";
			return await this.httpClient.GetAsync<WalletAddress>(route, queryString).ConfigureAwait(false);
		}

		/// <summary>
		/// Fetches the wallet balance. Includes unconfirmed transactions and possibly double spends.
		/// </summary>
		/// <returns>Wallet balance</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<BitcoinValue?> GetBalanceAsync()
		{
			var queryString = this.BuildBasicQueryString();
			var route = $"merchant/{this.identifier}/balance";
			return await this.httpClient.GetAsync<BitcoinValue>(route, queryString).ConfigureAwait(false);
		}

		/// <summary>
		/// Lists all active addresses in the wallet.
		/// </summary>
		/// <returns>A list of Address objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<List<WalletAddress>?> ListAddressesAsync()
		{
			var queryString = this.BuildBasicQueryString();

			var route = $"merchant/{this.identifier}/list";

			return await this.httpClient.GetAsync(route, queryString, WalletAddress.DeserializeMultiple).ConfigureAwait(false);
		}

		/// <summary>
		/// Generates a new address and adds it to the wallet.
		/// </summary>
		/// <param name="label">Label to attach to this address</param>
		/// <returns>An instance of the Address class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<WalletAddress?> NewAddressAsync(string? label = null)
		{
			var queryString = this.BuildBasicQueryString();
			if (label is not null)
			{
				queryString.Add("label", label);
			}

			var route = $"merchant/{this.identifier}/new_address";
			return await this.httpClient.GetAsync<WalletAddress>(route, queryString).ConfigureAwait(false);
		}

		/// <summary>
		/// Sends bitcoin from your wallet to a single address.
		/// </summary>
		/// <param name="toAddress">Recipient bitcoin address</param>
		/// <param name="amount">Amount to send</param>
		/// <param name="fromAddress">Specific address to send from</param>
		/// <param name="fee">Transaction fee. Must be greater than the default fee</param>
		/// <returns>An instance of the PaymentResponse class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<PaymentResponse?> SendAsync(string toAddress, BitcoinValue amount, string? fromAddress = null, BitcoinValue? fee = null)
		{
			if (string.IsNullOrWhiteSpace(toAddress))
			{
				throw new ArgumentNullException(nameof(toAddress));
			}

			if (amount.GetBtc() <= 0)
			{
				throw new ArgumentException("Amount sent must be greater than 0", nameof(amount));
			}

			var queryString = new QueryString();
			queryString.Add("password", this.password);
			queryString.Add("to", toAddress);
			queryString.Add("amount", amount.Satoshis.ToString(CultureInfo.CurrentCulture));
			if (!string.IsNullOrWhiteSpace(this.secondPassword))
			{
				queryString.Add("second_password", this.secondPassword);
			}

			if (!string.IsNullOrWhiteSpace(fromAddress))
			{
				queryString.Add("from", fromAddress);
			}

			if (fee is not null)
			{
				queryString.Add("fee", fee.ToString());
			}

			var route = $"merchant/{this.identifier}/payment";

			return await this.httpClient.GetAsync<PaymentResponse>(route, queryString).ConfigureAwait(false);
		}

		/// <summary>
		/// Sends bitcoin from your wallet to multiple addresses.
		/// </summary>
		/// <param name="recipients">Dictionary with the structure of 'address':amount (string:BitcoinValue)</param>
		/// <param name="fromAddress">Specific address to send from</param>
		/// <param name="fee">Transaction fee. Must be greater than the default fee</param>
		/// <returns>An instance of the PaymentResponse class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<PaymentResponse?> SendManyAsync(Dictionary<string, BitcoinValue> recipients, string? fromAddress = null, BitcoinValue? fee = null)
		{
			if (recipients is null || recipients.Count == 0)
			{
				throw new ArgumentException("Sending bitcoin from your wallet requires at least one receipient.", nameof(recipients));
			}

			var queryString = new QueryString();
			queryString.Add("password", this.password);
			var recipientsJson = JsonConvert.SerializeObject(recipients, Formatting.None, new BitcoinValueJsonConverter());
			queryString.Add("recipients", recipientsJson);
			if (!string.IsNullOrWhiteSpace(this.secondPassword))
			{
				queryString.Add("second_password", this.secondPassword);
			}

			if (!string.IsNullOrWhiteSpace(fromAddress))
			{
				queryString.Add("from", fromAddress);
			}

			if (fee is not null)
			{
				queryString.Add("fee", fee.ToString());
			}

			var route = $"merchant/{this.identifier}/sendmany";

			return await this.httpClient.GetAsync<PaymentResponse>(route, queryString).ConfigureAwait(false);
		}

		/// <summary>
		/// Unarchives an address.
		/// </summary>
		/// <param name="address">Address to unarchive</param>
		/// <returns>String representation of the unarchived address</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"><paramref name="address" /> is <c>null</c>.</exception>
		public async Task<string?> UnarchiveAddressAsync(string address)
		{
			if (address is null)
			{
				throw new ArgumentNullException(nameof(address));
			}

			var queryString = this.BuildBasicQueryString();
			queryString.Add("address", address);

			var route = $"merchant/{this.identifier}/unarchive_address";
			return await this.httpClient.GetAsync(route, queryString, WalletAddress.DeserializeUnArchived).ConfigureAwait(false);
		}

		private QueryString BuildBasicQueryString()
		{
			var queryString = new QueryString();

			queryString.Add("password", this.password);
			if (this.secondPassword is not null)
			{
				queryString.Add("second_password", this.secondPassword);
			}

			return queryString;
		}
	}
}
