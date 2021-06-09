namespace Info.Blockchain.API.Tests
{
	using Info.Blockchain.API.Client;

	using System;
	using System.Threading.Tasks;

	public class FakeHttpClient : IHttpClient
	{
		private bool disposedValue;

		~FakeHttpClient()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: false);
		}

		public string? ApiCode { get; set; }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			this.Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public Task<T?> GetAsync<T>(string route, QueryString? queryString = null, Func<string, T>? customDeserialization = null) => Task.FromResult(default(T));

		public Task<TResponse?> PostAsync<TPost, TResponse>(string route, TPost postObject, Func<string, TResponse>? customDeserialization = null, bool multiPartContent = false, string? contentType = null) =>
			Task.FromResult(default(TResponse));

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					// dispose managed state (managed objects)
				}

				// free unmanaged resources (unmanaged objects) and override finalizer set large
				// fields to null
				this.disposedValue = true;
			}
		}
	}
}
