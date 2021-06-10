﻿namespace Info.Blockchain.API.Tests.UnitTests
{
	using Info.Blockchain.API.Client;
	using Info.Blockchain.API.Models;

	using System;
	using System.Globalization;

	using Xunit;

	public class CurrencyTests
	{
		public CurrencyTests()
		{
			CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
		}

		[Fact]
		public async void ToBtc_NullCurrency_ArgumentNullException()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.exchangeRateExplorer.ToBtcAsync(null, 1);
				}
			});
		}

		[Fact]
		public async void ToBtc_NegativeValue_ArgumentOutOfRangeException()
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.exchangeRateExplorer.ToBtcAsync("USD", -1);
				}
			});
		}

		[Fact]
		public async void FromBtc_NegativeValue_ArgumentOutOfRangeException()
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					var btc = new BitcoinValue(new decimal(-0.4));
					await apiHelper.exchangeRateExplorer.FromBtcAsync(btc);
				}
			});

			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				using (BlockchainApiHelper apiHelper = UnitTestUtil.GetFakeHelper())
				{
					await apiHelper.exchangeRateExplorer.FromBtcAsync(null);
				}
			});
		}
	}
}
