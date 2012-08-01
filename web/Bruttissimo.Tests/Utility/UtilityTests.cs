using System;
using System.Drawing;
using System.Net;
using Bruttissimo.Common;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests
{
	[TestClass]
	public class UtilityTests
	{
		private HttpHelper httpHelper;
		private FileSystemHelper fsHelper;

		[TestInitialize]
		public void TestInit()
		{
			httpHelper = new HttpHelper();
			fsHelper = new FileSystemHelper();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DownloadHttpHeader_WithNull_ThrowsArgumentNullException()
		{
			// Act
			httpHelper.DownloadHttpHeader(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DownloadAsHtml_WithNull_ThrowsArgumentNullException()
		{
			// Act
			httpHelper.DownloadAsHtml(null);
		}


		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DownloadAsImage_WithNull_ThrowsArgumentNullException()
		{
			// Act
			httpHelper.DownloadAsImage(null);
		}

		private readonly Uri UriHtmlResource = new Uri("http://www.google.com");
		private readonly Uri UriImageResource = new Uri("http://cdn.sstatic.net/img/hosted/MEwvJ.png");

		[TestMethod]
		public void DownloadHttpHeader_WithUri_ReturnsHeaderCollection()
		{
			// Act
			WebHeaderCollection headers = httpHelper.DownloadHttpHeader(UriHtmlResource);

			// Assert
			Assert.IsNotNull(headers);
		}

		[TestMethod]
		public void DownloadAsHtml_WithHtmlResource_ReturnsHtmlDocument()
		{
			// Act
			HtmlDocument document = httpHelper.DownloadAsHtml(UriHtmlResource);

			// Assert
			Assert.IsNotNull(document);
		}

		[TestMethod]
		public void DownloadAsHtml_WithImageResource_ReturnsNull()
		{
			// Act
			HtmlDocument document = httpHelper.DownloadAsHtml(UriImageResource);

			// Assert
			Assert.IsNull(document);
		}

		[TestMethod]
		public void DownloadAsImage_WithImageResource_ReturnsImage()
		{
			// Act
			Image image = httpHelper.DownloadAsImage(UriImageResource);

			// Assert
			Assert.IsNotNull(image);
		}

		[TestMethod]
		public void DownloadAsImage_WithHtmlResource_ReturnsNull()
		{
			// Act
			Image image = httpHelper.DownloadAsImage(UriHtmlResource);

			// Assert
			Assert.IsNull(image);
		}

		[TestMethod]
		public void InternalGenerateRandomFilenameFormat_WithDateAndGuid_ReturnsFormatString()
		{
			// Arrange
			DateTime dateTime = DateTime.UtcNow;
			Guid guid = Guid.NewGuid();

			// Act
			string format = fsHelper.InternalGenerateRandomFilenameFormat(dateTime, guid);

			// Assert
			Assert.IsTrue(format == "{0}_{1}_{2}".FormatWith(dateTime.Ticks, "{0}", guid.Stringify()));
		}
	}
}