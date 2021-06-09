namespace Info.Blockchain.API.Client
{
	using System;

	/// <summary>
	/// The exception that is thrown when an error happens in the code for the Blockchain Api
	/// library, not the on the server that the code calls
	/// </summary>
	public class ClientApiException : ApiExceptionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ClientApiException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ClientApiException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClientApiException" /> class.
		/// </summary>
		protected ClientApiException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClientApiException" /> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference ( <see
		/// langword="Nothing" /> in Visual Basic) if no inner exception is specified.
		/// </param>
		protected ClientApiException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
