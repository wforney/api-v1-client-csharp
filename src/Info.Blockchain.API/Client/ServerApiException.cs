namespace Info.Blockchain.API.Client
{
	using System.Net;

	/// <summary>
	/// The class `ApiException` represents a failed call to the Blockchain API. Whenever the server
	/// is unable to process a request (usually due to parameter validation errors), an instance of
	/// this class is thrown.
	/// </summary>
	public class ServerApiException : ApiExceptionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServerApiException" /> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="statusCode">The status code.</param>
		public ServerApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message) => this.StatusCode = statusCode;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServerApiException" /> class.
		/// </summary>
		protected ServerApiException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ServerApiException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		protected ServerApiException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ServerApiException" /> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference ( <see
		/// langword="Nothing" /> in Visual Basic) if no inner exception is specified.
		/// </param>
		protected ServerApiException(string? message, System.Exception? innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Gets the status code.
		/// </summary>
		/// <value>The status code.</value>
		public HttpStatusCode StatusCode { get; }
	}
}
