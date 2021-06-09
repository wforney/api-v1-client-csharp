namespace Info.Blockchain.API.Client
{
	using System;

	/// <summary>
	/// The base exception for the BlockChain Api. Its only use is to detect if the exception came
	/// from the api rather that another source
	/// </summary>
	public abstract class ApiExceptionBase : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ApiExceptionBase" /> class.
		/// </summary>
		protected ApiExceptionBase()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiExceptionBase" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		protected ApiExceptionBase(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiExceptionBase" /> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference ( <see
		/// langword="Nothing" /> in Visual Basic) if no inner exception is specified.
		/// </param>
		protected ApiExceptionBase(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
