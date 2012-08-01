using System;
using System.IO;
using System.Net;

namespace Bruttissimo.Tests.Mocking
{
	public class TestWebRequestCreate : IWebRequestCreate
	{
		private static WebRequest _nextRequest;
		private static readonly object _lockObject = new object();

		public static WebRequest NextRequest
		{
			get { return _nextRequest; }
			set
			{
				lock (_lockObject)
				{
					_nextRequest = value;
				}
			}
		}

		public WebRequest Create(Uri uri)
		{
			return _nextRequest;
		}

		public static TestWebRequest CreateTestRequest(Stream responseStream)
		{
			TestWebRequest request = new TestWebRequest(responseStream);
			NextRequest = request;
			return request;
		}
	}
}