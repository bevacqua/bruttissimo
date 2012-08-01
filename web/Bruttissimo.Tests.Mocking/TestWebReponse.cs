using System;
using System.IO;
using System.Net;

namespace Bruttissimo.Tests.Mocking
{
	public class TestWebReponse : WebResponse
	{
		private readonly Stream responseStream;

		public TestWebReponse(Stream responseStream)
		{
			if (responseStream == null)
			{
				throw new ArgumentNullException("responseStream");
			}
			this.responseStream = responseStream;
		}

		public override Stream GetResponseStream()
		{
			return responseStream;
		}
	}
}