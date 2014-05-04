using System;
using System.Runtime.Serialization;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// The exception that is thrown when the license validation error occurs.
	/// </summary>
	[Serializable]
	public class LicenseValidationException : Exception
	{
		#region Construction
		/// <summary>
		/// Initializes a new instance of the LicenseValidationException class.
		/// </summary>
		public LicenseValidationException() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the LicenseValidationException class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public LicenseValidationException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LicenseValidationException class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		public LicenseValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LicenseValidationException class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected LicenseValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
		#endregion
	}
}
