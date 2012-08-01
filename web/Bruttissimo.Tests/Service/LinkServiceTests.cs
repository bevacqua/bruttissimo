using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Logic;
using Bruttissimo.Tests.Mocking;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bruttissimo.Tests
{
	[TestClass]
	public class LinkServiceTests
	{
		private ILinkService linkService;

		[TestInitialize]
		public void TestInit()
		{
			linkService = MockHelpers.FakeLinkService();
		}

		#region LinkService.GetUri

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetUris_WithNull_ThrowsArgumentNullException()
		{
			// Act
			linkService.GetReferenceUris(null);
		}

		[TestMethod]
		public void GetUris_WithEmptyString_ReturnsEmptyList()
		{
			// Act
			IList<Uri> result = linkService.GetReferenceUris(string.Empty);

			// Assert
			Assert.IsTrue(result.Count == 0);
		}

		private const string TextWithoutLinks = "This is a text without links even though you might think this is one shop.com, except it isn't.";

		[TestMethod]
		public void GetUris_WithRandomText_ReturnsEmptyList()
		{
			// Act
			IList<Uri> result = linkService.GetReferenceUris(TextWithoutLinks);

			// Assert
			Assert.IsTrue(result.Count == 0);
		}

		private const string TextWithSingleLink = "www.domain.com";

		[TestMethod]
		public void GetUris_WithSingleLink_ReturnsOneMatch()
		{
			// Act
			IList<Uri> result = linkService.GetReferenceUris(TextWithSingleLink);

			// Assert
			Assert.IsTrue(result.Count == 1);
		}

		private const string TextContainingSingleLink = "This contains a single link: www.domain.com but it's not alone in this world";

		[TestMethod]
		public void GetUris_WithTextContainingSingleLink_ReturnsOneMatch()
		{
			// Act
			IList<Uri> result = linkService.GetReferenceUris(TextContainingSingleLink);

			// Assert
			Assert.IsTrue(result.Count == 1);
		}

		private const string TextContainingTwoLinks = "This contains a couple of links: www.first.com, http://www.second.com and even a comma right after the first one!";

		[TestMethod]
		public void GetUris_WithTextContainingTwoLinks_ReturnsTwoMatches()
		{
			// Act
			IList<Uri> result = linkService.GetReferenceUris(TextContainingTwoLinks);

			// Assert
			Assert.IsTrue(result.Count == 2);
		}

		#endregion

		#region Link Parsing

		[TestMethod]
		public void LinkCrawlerMetadataShould_BeProperlyLineBroken()
		{
			// Arrange
			Uri endpoint = new Uri("http://stackoverflow.com/questions/11706125/how-to-keep-sticky-checkboxes-from-resetting-when-submitting-multiple-forms-on");
			ILinkCrawlerService linkCrawler = new LinkCrawlerService(new HttpHelper());

			// Act
			Link link = linkCrawler.CrawlHttpResource(endpoint);

			// Assert
			Assert.IsFalse(link.Description.Contains("\n\n\n"));
		}

		[TestMethod]
		public void LinkCrawlerMetadataShould_ActuallyGetTheThumbnail_ForCronica()
		{
			// Arrange
			Uri endpoint = new Uri("http://www.cronica.com.ar/diario/2012/07/30/30541-delpo-quiere-meterse-en-la-tercera-ronda.html");
			ILinkCrawlerService linkCrawler = new LinkCrawlerService(new HttpHelper());

			// Act
			Link link = linkCrawler.CrawlHttpResource(endpoint);

			// Assert
			Assert.AreEqual("http://www.cronica.com.ar/imgs_notas/2012/07/30/30541_114532_650x420.jpg", link.Picture);
		}

		[TestMethod]
		public void LinkCrawlerMetadataShould_BeProperlyDecoded_ForCronica()
		{
			// Arrange
			Uri endpoint = new Uri("http://www.cronica.com.ar/diario/2012/07/30/30541-delpo-quiere-meterse-en-la-tercera-ronda.html");
			ILinkCrawlerService linkCrawler = new LinkCrawlerService(new HttpHelper());

			// Act
			Link link = linkCrawler.CrawlHttpResource(endpoint);

			// Assert
			Assert.IsTrue(link.Description.Contains("cómodamente"));
		}

		[TestMethod]
		public void LinkCrawlerMetadataShould_BeProperlyDecoded_ForFacebook()
		{
			// Arrange
			Uri endpoint = new Uri("https://www.facebook.com/");
			HttpHelper helper = new HttpHelper();

			// Act
			HtmlDocument document = helper.DownloadAsHtml(endpoint);

			// Assert
			Assert.IsTrue(document.DocumentNode.OuterHtml.Contains("ñ"));
		}

		[TestMethod]
		public void LinkCrawlerMetadataShould_GetProperDescription_ForGuiaSkater()
		{
			// Arrange
			Uri endpoint = new Uri("http://www.guiaskater.com/spots/skatepark-monte-grande.html");
			ILinkCrawlerService linkCrawler = new LinkCrawlerService(new HttpHelper());

			// Act
			Link link = linkCrawler.CrawlHttpResource(endpoint);

			// Assert
			Assert.IsTrue(link.Description.Contains("banquitos"));
		}

		#endregion
	}
}
