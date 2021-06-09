using System.Collections.ObjectModel;
using Info.Blockchain.API.Models;
using Info.Blockchain.API.Client;
using KellermanSoftware.CompareNetObjects;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	/// <summary>
	/// The TransactionTests class.
	/// </summary>
	public class TransactionTests
	{
		/// <summary>
		/// Defines the test method GetTransaction_ByHash_Valid.
		/// </summary>
		[Fact(Skip = "Test freezes because of comparison tool")]
		public async void GetTransaction_ByHash_Valid()
		{
			using var apiHelper = new BlockchainApiHelper();
			var knownTransaction = ReflectionUtil.DeserializeFile<Transaction>("single_transaction");
			var receivedTransaction = await apiHelper.blockExplorer.GetTransactionByHashAsync(knownTransaction.Hash).ConfigureAwait(false);

			var compareLogic = new CompareLogic();
			var comparisonResult = compareLogic.Compare(knownTransaction, receivedTransaction);
			Assert.True(comparisonResult.AreEqual);
		}

		/// <summary>
		/// Defines the test method GetTransaction_ByIndex_Valid.
		/// </summary>
		[Fact(Skip = "Test freezes because of comparison tool")]
		public async void GetTransaction_ByIndex_Valid()
		{
			using var apiHelper = new BlockchainApiHelper();
			var knownTransaction = ReflectionUtil.DeserializeFile<Transaction>("single_transaction");
			var receivedTransaction = await apiHelper.blockExplorer.GetTransactionByIndexAsync(knownTransaction.Index).ConfigureAwait(false);

			var compareLogic = new CompareLogic();
			var comparisonResult = compareLogic.Compare(knownTransaction, receivedTransaction);
			Assert.True(comparisonResult.AreEqual);
		}

		/// <summary>
		/// Defines the test method GetUnconfirmedTransaction_Valid.
		/// </summary>
		[Fact]
		public async void GetUnconfirmedTransaction_Valid()
		{
			using var apiHelper = new BlockchainApiHelper();
			var unconfirmedTransactions = await apiHelper.blockExplorer.GetUnconfirmedTransactionsAsync().ConfigureAwait(false);

			Assert.NotNull(unconfirmedTransactions);
		}
	}
}
