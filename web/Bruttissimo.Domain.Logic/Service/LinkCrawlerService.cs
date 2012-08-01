using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Bruttissimo.Common;
using Bruttissimo.Common.Resources;
using HtmlAgilityPack;

namespace Bruttissimo.Domain.Logic
{
	public class LinkCrawlerService : ILinkCrawlerService
	{
		private const int relevantContentNodes = 6; // after this amount of nodes, assume they're not what we're looking for.
		private const int maxChildNodes = 15; // after this amount of children, assume this node is not one of the "primary" nodes we seek.
		private const int minDescriptionContentWeight = 15; // minimum content-to-HTML ratio for a node to be deemed relevant to our search.

		private readonly HttpHelper httpHelper;

		public LinkCrawlerService(HttpHelper httpHelper)
		{
			if (httpHelper == null)
			{
				throw new ArgumentNullException("httpHelper");
			}
			this.httpHelper = httpHelper;
		}

		/// <summary>
		/// Crawls an HTTP endpoint and returns an entity that identifies its resource type and metadata related to it.
		/// </summary>
		public Link CrawlHttpResource(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			HtmlDocument document = httpHelper.DownloadAsHtml(uri);
			if (document != null)
			{
				return CreateLinkFromHtmlDocument(document, uri);
			}
			Image image = httpHelper.DownloadAsImage(uri);
			if (image != null)
			{
				return CreateLinkFromImageSource(image, uri);
			}
			return null;
		}

		/// <summary>
		/// Crawls an HTML document building metadata based on conventions or using a best-guess approach.
		/// </summary>
		internal virtual Link CreateLinkFromHtmlDocument(HtmlDocument document, Uri uri)
		{
			RemoveUndesirableTags(document);

			string title = CrawlUsingMetadata(XPath.ResourceTitle, document);
			string description = GetDescriptionFor(document);
			string picture = GetPictureFor(document, uri);

			Link link = new Link
			{
				Created = DateTime.UtcNow,
				ReferenceUri = uri.AbsoluteUri,
				Type = LinkType.Html,
				Title = HtmlEntity.DeEntitize(title),
				Description = HtmlEntity.DeEntitize(description),
				Picture = picture
			};
			return link;
		}

		/// <summary>
		/// Removes content-less tags that could end up deceiving our crawling implementation, such as script, and style tags.
		/// </summary>
		private void RemoveUndesirableTags(HtmlDocument document)
		{
			IEnumerable<HtmlNode> nodes = document.DocumentNode.SelectNodesOrEmpty(XPath.UndesirableHtmlTags);
			foreach (HtmlNode node in nodes)
			{
				node.Remove();
			}
		}

		/// <summary>
		/// Saves the image to the file system and stores the path in the link entity.
		/// </summary>
		internal virtual Link CreateLinkFromImageSource(Image image, Uri uri)
		{
			Link link = new Link
			{
				Created = DateTime.UtcNow,
				ReferenceUri = uri.AbsoluteUri,
				Type = LinkType.Image
			};
			return link;
		}

		/// <summary>
		/// Crawls an HTML document using XPath expressions, using the provided order.
		/// </summary>
		internal string CrawlUsingMetadata(IEnumerable<string> paths, HtmlDocument document, int? relevance = null)
		{
			foreach (string path in paths)
			{
				HtmlNode node = BestNodeAtXPath(document, path, relevance);
				if (node == null)
				{
					continue;
				}
				string result;
				if (node.Name.InsensitiveEquals("meta"))
				{
					result = node.GetAttributeValue("content", string.Empty);
				}
				else if (node.Name.InsensitiveEquals("link"))
				{
					result = node.GetAttributeValue("href", string.Empty);
				}
				else
				{
					result = node.InnerText;
				}
				return TrimmedOrNull(result);
			}
			return null;
		}

		private HtmlNode BestNodeAtXPath(HtmlDocument document, string path, int? relevance = null)
		{
			HtmlNode root = document.DocumentNode;
			IEnumerable<HtmlNode> nodes = root.SelectNodesOrEmpty(path);
			if (relevance.HasValue)
			{
				nodes = nodes.Where(n => NodeContentWeight(n) > relevance.Value); // some nodes are actually almost empty if we ignore their HTML.
				nodes = nodes.Where(n => n.ChildNodes.Count <= maxChildNodes); // some nodes have a massive amount of children, these are often false positives, so we're better off ignoring them.
			}
			nodes = nodes.OrderByDescending(n => n.GetDepth()).ThenBy(NodeContentWeight).Take(relevantContentNodes);
			IEnumerable<HtmlNode> orderedByEstimatedValue = nodes.OrderByDescending(EstimatedImportanceIndex);
			HtmlNode node = orderedByEstimatedValue.FirstOrDefault();
			return node;
		}

		private double NodeContentWeight(HtmlNode node)
		{
			int total = Math.Max(node.InnerHtml.Length, 1);
			double content = node.InnerText.TrimAll().Length;
			return content / total * 100;
		}

		private double EstimatedImportanceIndex(HtmlNode node)
		{
			int depth = node.GetDepth();
			int children = node.ChildNodes.Count;
			double content = NodeContentWeight(node);
			double weight = depth + children * 2.4 + content * .4;
			return weight;
		}

		private string GetDescriptionFor(HtmlDocument document)
		{
			string description = CrawlUsingMetadata(XPath.ResourceDescription, document, minDescriptionContentWeight);

			if (description == null) // sanity
			{
				return null;
			}
			string result = CompiledRegex.DistinctLineBreaks.Replace(description, Regex.DistinctLineBreaksReplacement);
			string decoded = HttpUtility.HtmlDecode(result);
			return decoded;
		}

		private string GetPictureFor(HtmlDocument document, Uri baseUri)
		{
			IEnumerable<Func<string>> sources = new List<Func<string>>
			{
				() => CrawlUsingMetadata(XPath.ResourcePicture, document),
				() => GetPictureByBestGuess(document) // if no metadata regarding a picture is present, fallback to best-guess from image tags.
			};
			foreach (Func<string> inspectSource in sources)
			{
				string picture = inspectSource();
				if (picture == null)
				{
					continue;
				}
				// we don't want it throwing if the "picture" is JavaScript.
				string uri = httpHelper.GetAbsoluteUriText(picture, baseUri, false);
				if (httpHelper.TryRequestImage(uri)) // make sure the link isn't broken or inaccessible.
				{
					return uri;
				}
			}
			return null;
		}

		/// <summary>
		/// Attempts to guess the thumbnail image based on the position of the resource's description (based on a convention).
		/// </summary>
		internal string GetPictureByBestGuess(HtmlDocument document)
		{
			HtmlNode root = document.DocumentNode;
			HtmlNode description = BestNodeAtXPath(document, XPath.ResourceDescriptionNodes, minDescriptionContentWeight);
			if (description == null)
			{
				return null;
			}
			// in this case, we determine the most relevant image to be the one closest to the description text node.
			HtmlNode parent = description.ParentNode;
			while (parent != null && parent.XPath != XPath.DocumentRoot)
			{
				string path = string.Concat(parent.XPath, XPath.ImageNode);
				IList<HtmlNode> images = root.SelectNodesOrEmpty(path).ToList();

				if (!images.Any())
				{
					parent = parent.ParentNode;
					continue;
				}
				// find the image node that's closest to the text node.
				HtmlNode image = description.FindClosestNode(parent, images);
				if (image != null) // sanity, do HTML documents ever have no relevant images?
				{
					string result = image.GetAttributeValue("src", string.Empty);
					return TrimmedOrNull(result);
				}
			}
			return null; // no metadata, no description, we can only do so much.
		}

		private string TrimmedOrNull(string text)
		{
			if (text != null)
			{
				text = text.Trim();

				if (text.Length != 0)
				{
					return text;
				}
			}
			return null;
		}
	}
}