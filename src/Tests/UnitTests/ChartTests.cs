namespace Info.Blockchain.API.Tests.UnitTests
{
	using Info.Blockchain.API.Client;

	using System;

	using Xunit;

	public class ChartTests
	{
		[Fact]
		public async void GetChart_WrongName_OutOfRangeException()
		{
			// TODO: This no longer returns a JSON string. It returns a web page.
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.statisticsExplorer.GetChartAsync("wrong-chart-name").ConfigureAwait(false);
				}
			}).ConfigureAwait(false);
		}

		[Fact]
		public async void GetChart_WrongTimespan_OutOfRangeException()
		{
			// TODO: This no longer returns a JSON string. It returns a web page.
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.statisticsExplorer.GetChartAsync("hash-rate", "wrong-timespan-format").ConfigureAwait(false);
				}
			}).ConfigureAwait(false);

			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.statisticsExplorer.GetPoolsAsync(0).ConfigureAwait(false);
				}
			}).ConfigureAwait(false);
		}
	}
}
