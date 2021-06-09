namespace Info.Blockchain.API.BlockExplorer
{
	using Info.Blockchain.API.Client;
	using Info.Blockchain.API.Json;
	using Info.Blockchain.API.Models;

	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// The BlockExplorer class reflects the functionality documented at
	/// https://blockchain.info/api/blockchain_api. It can be used to query the block chain, fetch
	/// block, transaction and address data, get unspent outputs for an address etc.
	/// </summary>
	public class BlockExplorer
	{
		/// <summary>
		/// The default unspent transactions per request
		/// </summary>
		public const int DefaultUnspentTransactionsPerRequest = 250;

		/// <summary>
		/// The maximum transactions per multi request
		/// </summary>
		public const int MaxTransactionsPerMultiRequest = 100;

		/// <summary>
		/// The maximum transactions per request
		/// </summary>
		public const int MaxTransactionsPerRequest = 50;

		/// <summary>
		/// The HTTP client
		/// </summary>
		private readonly IHttpClient httpClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="BlockExplorer" /> class.
		/// </summary>
		public BlockExplorer() => this.httpClient = new BlockchainHttpClient();

		/// <summary>
		/// Initializes a new instance of the <see cref="BlockExplorer" /> class.
		/// </summary>
		/// <param name="httpClient">The HTTP client.</param>
		internal BlockExplorer(IHttpClient httpClient) => this.httpClient = httpClient;

		/// <summary>
		/// Gets data for a single Base58Check address asynchronously.
		/// </summary>
		/// <param name="address">Base58Check address string</param>
		/// <param name="limit">Max amount of transactions to retrieve (Max 50)</param>
		/// <param name="offset">Number of transactions to skip</param>
		/// <param name="filter">Filter type to use for query</param>
		/// <returns>An instance of the Address class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<Address> GetBase58AddressAsync(
			string address,
			int limit = MaxTransactionsPerRequest,
			int offset = 0,
			FilterType filter = FilterType.RemoveUnspendable) =>
			await this.GetAddressAsync(address, limit, offset, filter).ConfigureAwait(false);

		/// <summary>
		/// Gets a single block based on a block hash.
		/// </summary>
		/// <param name="hash">Block hash</param>
		/// <returns>An instance of the Block class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<Block> GetBlockByHashAsync(string? hash) =>
			string.IsNullOrWhiteSpace(hash)
				? throw new ArgumentNullException(nameof(hash))
				: await this.GetBlockAsync(hash).ConfigureAwait(false);

		/// <summary>
		/// Deprecated. Gets a single block based on a block index.
		/// </summary>
		/// <param name="index">Block index</param>
		/// <returns>An instance of the Block class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		[Obsolete("Deprecated. Get block by hash wherever possible.")]
		public async Task<Block> GetBlockByIndexAsync(long index) =>
			index < 0
				? throw new ArgumentOutOfRangeException(nameof(index), "Index must be greater than zero")
				: await this.GetBlockAsync(index.ToString()).ConfigureAwait(false);

		/// <summary>
		/// Gets a list of blocks at the specified height. Normally, only one block will be
		/// returned, but in case of a chain fork, multiple blocks may be present.
		/// </summary>
		/// <param name="height">Block height</param>
		/// <returns>A list of blocks at the specified height</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public async Task<ReadOnlyCollection<Block?>?> GetBlocksAtHeightAsync(long height)
		{
			if (height < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(height), "Block height must be greater than or equal to zero");
			}

			var queryString = new QueryString();
			queryString.Add("format", "json");

			return await this.httpClient.GetAsync($"block-height/{height}", queryString, Block.DeserializeMultiple).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets a list of blocks mined on a specific day.
		/// </summary>
		/// <param name="dateTime">
		/// DateTime that falls between 00:00 UTC and 23:59 UTC of the desired day.
		/// </param>
		/// <returns>A list of SimpleBlock objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public async Task<ReadOnlyCollection<SimpleBlock>> GetBlocksByDateTimeAsync(DateTime dateTime)
		{
			DateTimeOffset utcDate = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
			var unixMillis = utcDate.ToUnixTimeMilliseconds();

			if (unixMillis < UnixDateTimeJsonConverter.GenesisBlockUnixMillis)
			{
				throw new ArgumentOutOfRangeException(nameof(dateTime), "Date must be greater than or equal to the genesis block creation date (2009-01-03T18:15:05+00:00)");
			}

			if (dateTime.ToUniversalTime() > DateTime.UtcNow)
			{
				throw new ArgumentOutOfRangeException(nameof(dateTime), "Date must be in the past");
			}

			return await this.GetBlocksAsync(unixMillis.ToString(CultureInfo.CurrentCulture)).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets a list of recent blocks by a specific mining pool (Max 101). No input returns all
		/// blocks mined today since midnight.
		/// </summary>
		/// <param name="poolName">Name of mining pool</param>
		/// <returns>A list of SimpleBlock objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<ReadOnlyCollection<SimpleBlock>> GetBlocksByPoolNameAsync(string poolName = "") => await this.GetBlocksAsync(poolName).ConfigureAwait(false);

		/// <summary>
		/// Gets a list of blocks mined on a specific day.
		/// </summary>
		/// <param name="unixMillis">
		/// Unix timestamp in milliseconds that falls between 00:00 UTC and 23:59 UTC of the desired day.
		/// </param>
		/// <returns>A list of SimpleBlock objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public async Task<ReadOnlyCollection<SimpleBlock>> GetBlocksByTimestampAsync(long unixMillis) =>
			unixMillis < UnixDateTimeJsonConverter.GenesisBlockUnixMillis
				? throw new ArgumentOutOfRangeException(nameof(unixMillis), "Date must be greater than or equal to the genesis block creation date (2009-01-03T18:15:05+00:00)")
				: await this.GetBlocksAsync(unixMillis.ToString()).ConfigureAwait(false);

		/// <summary>
		/// Gets data for a single Hash160 address asynchronously.
		/// </summary>
		/// <param name="address">Hash160 address string</param>
		/// <param name="limit">Max amount of transactions to retrieve (Max 50)</param>
		/// <param name="offset">Number of transactions to skip</param>
		/// <param name="filter">Filter type to use for query</param>
		/// <returns>An instance of the Address class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<Address> GetHash160AddressAsync(string address, int limit = MaxTransactionsPerRequest, int offset = 0, FilterType filter = FilterType.RemoveUnspendable) =>
			await this.GetAddressAsync(address, limit, offset, filter).ConfigureAwait(false);

		/// <summary>
		/// Gets the latest block on the main chain (simplified representation).
		/// </summary>
		/// <returns>An instance of the LatestBlock class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<LatestBlock?> GetLatestBlockAsync() => await this.httpClient.GetAsync<LatestBlock>("latestblock").ConfigureAwait(false);

		/// <summary>
		/// Gets data for multiple Base58Check and / or Xpub address asynchronously.
		/// </summary>
		/// <param name="addressList">IEnumerable of Base58Check and / or xPub address strings</param>
		/// <param name="limit">Max amount of transactions to retrieve (Max 50)</param>
		/// <param name="offset">Number of transactions to skip</param>
		/// <param name="filter">Filter type to use for query</param>
		/// <returns>An instance of the Address class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<MultiAddress?> GetMultiAddressAsync(IEnumerable<string> addressList, int limit = MaxTransactionsPerMultiRequest, int offset = 0, FilterType filter = FilterType.RemoveUnspendable)
		{
			if (addressList?.Any() != true)
			{
				throw new ArgumentNullException(nameof(addressList), "No addresses provided");
			}

			if (limit is < 1 or > MaxTransactionsPerMultiRequest)
			{
				throw new ArgumentOutOfRangeException(nameof(limit), $"transaction limit must be greater than 0 and smaller than {MaxTransactionsPerMultiRequest}");
			}

			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(offset), "offset must be equal to or greater than 0");
			}

			var queryString = new QueryString();
			var addressQuery = string.Join("|", addressList);

			queryString.Add("active", addressQuery);
			queryString.Add("limit", limit.ToString(CultureInfo.CurrentCulture));
			queryString.Add("offset", offset.ToString(CultureInfo.CurrentCulture));
			queryString.Add("filter", ((int)filter).ToString(CultureInfo.CurrentCulture));
			queryString.Add("format", "json");

			try
			{
				return await this.httpClient.GetAsync<MultiAddress>("multiaddr", queryString).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("Invalid Bitcoin Address"))
				{
					throw new ArgumentException("one or more addresses provided are invalid", nameof(addressQuery));
				}

				throw;
			}
		}

		/// <summary>
		/// Gets a single transaction based on a transaction hash.
		/// </summary>
		/// <param name="hash">Transaction hash</param>
		/// <returns>An instance of the Transaction class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		public async Task<Transaction> GetTransactionByHashAsync(string hash)
		{
			return string.IsNullOrWhiteSpace(hash)
				? throw new ArgumentNullException(nameof(hash))
				: await this.GetTransactionAsync(hash).ConfigureAwait(false);
		}

		/// <summary>
		/// Deprecated. Gets a single transaction based on a transaction index.
		/// </summary>
		/// <param name="index">Transaction index</param>
		/// <returns>An instance of the Transaction class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		[Obsolete("Deprecated. Get transaction by hash wherever possible.")]
		public async Task<Transaction> GetTransactionByIndexAsync(long index) =>
			index < 0
			? throw new ArgumentOutOfRangeException(nameof(index), "Index must be a positive integer")
			: await this.GetTransactionAsync(index.ToString()).ConfigureAwait(false);

		/// <summary>
		/// Gets a list of the last 10 unconfirmed transactions.
		/// </summary>
		/// <returns>A list of unconfirmed Transaction objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<ReadOnlyCollection<Transaction>?> GetUnconfirmedTransactionsAsync()
		{
			var queryString = new QueryString();
			queryString.Add("format", "json");

			return await this.httpClient.GetAsync("unconfirmed-transactions", queryString, Transaction.DeserializeMultiple).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets unspent outputs for a one or more Base58Check and / or xPub addresses.
		/// </summary>
		/// <param name="addressList">IEnumerable of Base58check and / or xPub address strings</param>
		/// <param name="limit">Max amount of unspent outputs to receive (Max 50)</param>
		/// <param name="confirmations">Minimum number of confirmations to receive (Default 0)</param>
		/// <returns>A list of unspent outputs for the specified address</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<ReadOnlyCollection<UnspentOutput>?> GetUnspentOutputsAsync(IEnumerable<string> addressList, int limit = DefaultUnspentTransactionsPerRequest, int confirmations = 0)
		{
			if (addressList?.Any() != true)
			{
				throw new ArgumentNullException(nameof(addressList), "No addresses provided");
			}

			if (limit is < 1 or > DefaultUnspentTransactionsPerRequest)
			{
				throw new ArgumentOutOfRangeException(nameof(limit), $"transaction limit must be greater than 0 and smaller than {DefaultUnspentTransactionsPerRequest}");
			}

			if (confirmations < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(confirmations), "confirmations must be equal to or greater than 0");
			}

			var queryString = new QueryString();
			var addressQuery = string.Join("|", addressList);

			queryString.Add("active", addressQuery);
			queryString.Add("limit", limit.ToString(CultureInfo.CurrentCulture));
			queryString.Add("confirmations", confirmations.ToString(CultureInfo.CurrentCulture));
			queryString.Add("format", "json");
			try
			{
				return await this.httpClient.GetAsync("unspent", queryString, UnspentOutput.DeserializeMultiple).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				// Currently the API throws an internal error if there are no free outputs to spend.
				// No free outputs is a legitimate response. Therefore, until we fix this issue, we
				// are circumventing this by returning an empty list
				if (ex.Message.Contains("outputs to spend"))
				{
					return new ReadOnlyCollection<UnspentOutput>(new List<UnspentOutput>());
				}

				if (ex.Message.Contains("Invalid Bitcoin Address"))
				{
					throw new ArgumentException("one or more addresses provided are invalid", nameof(addressQuery));
				}

				throw;
			}
		}

		/// <summary>
		/// Returns xpub summary on a xpub provided, with its overall balance and its transactions.
		/// </summary>
		/// <param name="xpub">Xpub address string</param>
		/// <param name="limit">Max amount of transactions to retrieve (Max 50)</param>
		/// <param name="offset">Number of transactions to skip</param>
		/// <param name="filter">Filter type to use for query</param>
		/// <returns>Xpub model</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<Xpub?> GetXpub(string xpub, int limit = MaxTransactionsPerMultiRequest, int offset = 0, FilterType filter = FilterType.RemoveUnspendable)
		{
			if (string.IsNullOrWhiteSpace(xpub))
			{
				throw new ArgumentNullException(xpub);
			}

			if (limit is < 1 or > MaxTransactionsPerMultiRequest)
			{
				throw new ArgumentOutOfRangeException(nameof(limit), $"transaction limit must be greater than 0 and smaller than {MaxTransactionsPerMultiRequest}");
			}

			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(offset), "offset must be equal to or greater than 0");
			}

			var queryString = new QueryString();

			queryString.Add("active", xpub);
			queryString.Add("limit", limit.ToString(CultureInfo.CurrentCulture));
			queryString.Add("offset", offset.ToString(CultureInfo.CurrentCulture));
			queryString.Add("filter", ((int)filter).ToString(CultureInfo.CurrentCulture));
			queryString.Add("format", "json");

			try
			{
				return await this.httpClient.GetAsync("multiaddr", queryString, Xpub.Deserialize).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("Invalid Bitcoin Address"))
				{
					throw new ArgumentException("the xpub provided is invalid", nameof(xpub));
				}

				throw;
			}
		}

		/// <summary>
		/// get address as an asynchronous operation.
		/// </summary>
		/// <param name="address">The address.</param>
		/// <param name="limit">The limit.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="ft">The ft.</param>
		/// <returns>Address.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// limit - transaction limit must be greater than 0 and smaller than " +
		/// MAX_TRANSACTIONS_PER_REQUEST or offset - offset must be equal to or greater than 0
		/// </exception>
		/// <exception cref="ArgumentException">address - address provided is invalid</exception>
		private async Task<Address> GetAddressAsync(string address, int limit, int offset, FilterType ft)
		{
			if (string.IsNullOrWhiteSpace(address))
			{
				throw new ArgumentNullException(address);
			}

			if (limit is < 1 or > MaxTransactionsPerRequest)
			{
				throw new ArgumentOutOfRangeException(nameof(limit), $"transaction limit must be greater than 0 and smaller than {MaxTransactionsPerRequest}");
			}

			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(offset), "offset must be equal to or greater than 0");
			}

			var queryString = new QueryString();
			queryString.Add("limit", limit.ToString(CultureInfo.CurrentCulture));
			queryString.Add("offset", offset.ToString(CultureInfo.CurrentCulture));
			queryString.Add("filter", ((int)ft).ToString(CultureInfo.CurrentCulture));
			queryString.Add("format", "json");

			try
			{
				return await this.httpClient.GetAsync<Address>("address/" + address, queryString).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("does not validate") || ex.Message.Contains("too short"))
				{
					throw new ArgumentException("address provided is invalid", nameof(address));
				}

				throw;
			}
		}

		/// <summary>
		/// get block as an asynchronous operation.
		/// </summary>
		/// <param name="hashOrIndex">Index of the hash or.</param>
		/// <returns>Block.</returns>
		/// <exception cref="ArgumentNullException">hashOrIndex</exception>
		private async Task<Block> GetBlockAsync(string hashOrIndex) =>
			string.IsNullOrWhiteSpace(hashOrIndex)
				? throw new ArgumentNullException(nameof(hashOrIndex))
				: await this.httpClient.GetAsync($"rawblock/{hashOrIndex}", customDeserialization: Block.Deserialize).ConfigureAwait(false);

		/// <summary>
		/// Gets a list of recent blocks by a specific mining pool or up to a specified timestamp.
		/// If neither is provided returns all blocks mined since midnight.
		/// </summary>
		/// <param name="poolNameOrTimestamp">
		/// Name of the mining pool or Unix timestamp in milliseconds that falls between 00:00 UTC
		/// and 23:59 UTC of the desired day.
		/// </param>
		/// <returns>A list of SimpleBlock objects</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		private async Task<ReadOnlyCollection<SimpleBlock>> GetBlocksAsync(string poolNameOrTimestamp)
		{
			var queryString = new QueryString();
			queryString.Add("format", "json");

			return await this.httpClient.GetAsync($"blocks/{poolNameOrTimestamp}", queryString, SimpleBlock.DeserializeMultiple).ConfigureAwait(false);
		}

		/// <summary>
		/// get transaction as an asynchronous operation.
		/// </summary>
		/// <param name="hashOrIndex">Index of the hash or.</param>
		/// <returns>Transaction.</returns>
		/// <exception cref="ArgumentNullException">hashOrIndex</exception>
		private async Task<Transaction> GetTransactionAsync(string hashOrIndex) =>
			string.IsNullOrWhiteSpace(hashOrIndex)
				? throw new ArgumentNullException(nameof(hashOrIndex))
				: await this.httpClient.GetAsync<Transaction>($"rawtx/{hashOrIndex}").ConfigureAwait(false);
	}
}
