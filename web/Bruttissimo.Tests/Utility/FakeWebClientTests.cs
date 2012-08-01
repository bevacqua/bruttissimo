using System;
using System.IO;
using Bruttissimo.Common;
using Bruttissimo.Tests.Mocking;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests
{
	[TestClass]
	public class FakeWebClientTests
	{
		[TestInitialize]
		public void TestInit()
		{
			MockHelpers.SetupFakeWebClient();
		}

		private readonly Uri FakeWebClientTestUri = new Uri("test://fake-web-client-test");

		[TestMethod]
		public void FakeWebRequest_WithTestUri_ReturnsSameResponse()
		{
			// Arrange
			Uri uri = FakeWebClientTestUri;
			Stream fakeResponseStream = MockHelpers.GetFakeResponseStream(uri);
			TestWebRequestCreate.CreateTestRequest(fakeResponseStream);

			// Act
			string actualResponse;
			using (ExtendedWebClient client = new ExtendedWebClient())
			{
				using (Stream stream = client.OpenRead(uri))
				{
					actualResponse = stream.ReadFully();
				}
			}

			// Assert
			Assert.AreEqual(fakeResponseStream.ReadFully(), actualResponse);
		}

		[TestMethod]
		public void FakeWebRequest_WithTestUri_ReturnsExpectedResponse()
		{
			// Arrange
			Uri uri = FakeWebClientTestUri;
			Stream fakeResponseStream = MockHelpers.GetFakeResponseStream(uri);
			TestWebRequestCreate.CreateTestRequest(fakeResponseStream);

			// Act
			string actualResponse;
			using (ExtendedWebClient client = new ExtendedWebClient())
			{
				using (Stream stream = client.OpenRead(uri))
				{
					actualResponse = stream.ReadFully();
				}
			}

			// Assert
			Assert.AreEqual("This is the fake response in the text file.", actualResponse);
		}
	}
}
