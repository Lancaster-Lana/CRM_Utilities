using System;
using System.Runtime.Serialization;

namespace LanaSoft.Licensing
{
	/// <summary>
	/// The exception that is thrown when the license is expired.
	/// </summary>
	[Serializable]
	public class LicenseExpiredException : Exception
	{
		#region Construction
		/// <summary>
		/// Initializes a new instance of the LicenseExpiredException class.
		/// </summary>
		public LicenseExpiredException() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the LicenseExpiredException class with a specified error message.
		/// </summary>
		/// <param name="message">A message that describes the error.</param>
		public LicenseExpiredException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LicenseExpiredException class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
		public LicenseExpiredException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LicenseExpiredException class with serialized data.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		protected LicenseExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
		#endregion
	}
}
