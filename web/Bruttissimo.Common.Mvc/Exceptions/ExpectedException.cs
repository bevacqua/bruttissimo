using System;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// Expected exceptions thrown during server requests have their message passed down to the client as part of the response.
	/// </summary>
	public class ExpectedException : ApplicationException
	{
		/// <summary>
		/// Expected exceptions thrown during server requests have their message passed down to the client as part of the response.
		/// </summary>
		public ExpectedException()
		{
		}

		/// <summary>
		/// Expected exceptions thrown during server requests have their message passed down to the client as part of the response.
		/// </summary>
		public ExpectedException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Expected exceptions thrown during server requests have their message passed down to the client as part of the response.
		/// </summary>
		public ExpectedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}