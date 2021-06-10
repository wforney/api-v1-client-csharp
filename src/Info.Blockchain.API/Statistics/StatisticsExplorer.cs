namespace Info.Blockchain.API.Statistics
{
	using Info.Blockchain.API.Client;
	using Info.Blockchain.API.Models;

	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// This class allows users to get the bitcoin network statistics.
	/// It reflects the functionality documented at https://blockchain.info/api/charts_api
	/// </summary>
	public class StatisticsExplorer : IDisposable
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
		/// Initializes a new instance of the <see cref="StatisticsExplorer"/> class.
		/// </summary>
		public StatisticsExplorer() => this.httpClient = new BlockchainHttpClient(uri: "https://api.blockchain.info");

		/// <summary>
		/// Initializes a new instance of the <see cref="StatisticsExplorer"/> class.
		/// </summary>
		/// <param name="httpClient">The HTTP client.</param>
		internal StatisticsExplorer(IHttpClient httpClient) => this.httpClient = httpClient;

		/// <summary>
		/// Gets the network statistics.
		/// </summary>
		/// <returns>An instance of the StatisticsResponse class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<StatisticsResponse?> GetStatsAsync()
		{
			var queryString = new QueryString();
			queryString.Add("format", "json");
			return await this.httpClient.GetAsync<StatisticsResponse>("stats", queryString).ConfigureAwait(false);
		}

		/// <summary>
		/// Gets chart data for a specified chart
		/// </summary>
		/// <param name="chartType">Chart name</param>
		/// <param name="timespan">Optional timespan to include</param>
		/// <param name="rollingAverage">Optional duration over which data should be averaged</param>
		/// <returns>Chart response object</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public async Task<ChartResponse?> GetChartAsync(string chartType, string? timespan = null, string? rollingAverage = null)
		{
			var queryString = new QueryString();
			queryString.Add("format", "json");
			if (timespan is not null)
			{
				queryString.Add("timespan", timespan);
			}

			if (rollingAverage is not null)
			{
				queryString.Add("rollingAverage", rollingAverage);
			}

			try
			{
				return await this.httpClient.GetAsync<ChartResponse>($"charts/{chartType}", queryString).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("No chart with this name"))
				{
					throw new ArgumentOutOfRangeException(nameof(chartType), "This chart name does not exist");
				}

				if (ex.Message.Contains("Not Found"))
				{
					throw new ArgumentOutOfRangeException(nameof(chartType), "This chart name does not exist");
				}

				if (ex.Message.Contains("Could not parse timestring"))
				{
					throw new ArgumentOutOfRangeException(nameof(timespan), "Incorrect timespan format");
				}

				throw;
			}
		}

		/// <summary>
		/// Get a dictionary of pool names and the number of blocks
		/// mined in the last `timespan` days
		/// </summary>
		/// <param name="timespan">Number of days to display mined blocks for</param>
		/// <returns>A dictionary of pool names and number of blocks mined</returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public async Task<IDictionary<string, int>?> GetPoolsAsync(int timespan = 4)
		{
			if (timespan is < 1 or > 10)
			{
				throw new ArgumentOutOfRangeException(nameof(timespan), "Timespan must be between 1 to 10");
			}

			var queryString = new QueryString();
			queryString.Add("format", "json");
			queryString.Add("timespan", timespan + "days");

			return await this.httpClient.GetAsync<Dictionary<string, int>>("pools", queryString).ConfigureAwait(false);
		}

		/// <summary>
		/// Disposes this instance.
		/// </summary>
		/// <param name="disposing">The disposing.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					// dispose managed state (managed objects)
					this.httpClient?.Dispose();
				}

				// free unmanaged resources (unmanaged objects) and override finalizer
				// set large fields to null
				this.disposedValue = true;
			}
		}

		/// <summary>
		/// Finalizes this instance.
		/// </summary>
		~StatisticsExplorer()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: false);
		}

		/// <inheritdoc />
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
