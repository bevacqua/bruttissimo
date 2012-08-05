using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
	public class LinkService : ILinkService
	{
		private readonly ILinkRepository linkRepository;
		private readonly ILinkCrawlerService linkCrawler;
		private readonly HttpHelper httpHelper;

		public LinkService(ILinkRepository linkRepository, ILinkCrawlerService linkCrawler, HttpHelper httpHelper)
		{
			if (linkRepository == null)
			{
				throw new ArgumentNullException("linkRepository");
			}
			if (linkCrawler == null)
			{
				throw new ArgumentNullException("linkCrawler");
			}
			if (httpHelper == null)
			{
				throw new ArgumentNullException("httpHelper");
			}
			this.linkRepository = linkRepository;
			this.linkCrawler = linkCrawler;
			this.httpHelper = httpHelper;
		}

		/// <summary>
		/// Parses a user-input text, inserts the records on the persister for future use, and returns the first occurrence of a web resource link.
		/// </summary>
		public LinkResult ParseUserInput(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			IList<Uri> uris = GetReferenceUris(text);
			LinkResult result = GetExistingLinkOrCrawlResource(uris);
			return result;
		}

		/// <summary>
		/// Extracts uris from a text input and returns them sanitized.
		/// </summary>
		public IList<Uri> GetReferenceUris(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			MatchCollection matches = CompiledRegex.WebLink.Matches(text);
			IList<Uri> uris = matches
				.Cast<Match>()
				.Select(match => match.Captures[0].Value)
				.Select(httpHelper.ConvertToUri)
				.ToList();

			return uris;
		}

		internal LinkResult GetExistingLinkOrCrawlResource(IEnumerable<Uri> uris)
		{
			if (uris == null)
			{
				throw new ArgumentNullException("uris");
			}
			Uri uri = uris.FirstOrDefault();
			if (uri == null)
			{
				return Invalid();
			}
			Link link = linkRepository.GetByReferenceUri(uri);
			if (link == null)
			{
				link = linkCrawler.CrawlHttpResource(uri);
				if (link == null)
				{
					return Invalid();
				}
				else
				{
					linkRepository.Insert(link);
					return new LinkResult { Result = LinkParseResult.Valid, Link = link };
				}
			}
			else
			{
				return new LinkResult
				{
					Result = link.PostId == null ? LinkParseResult.Valid : LinkParseResult.Used,
					Link = link,
				};
			}
		}

		internal LinkResult Invalid()
		{
			return new LinkResult { Result = LinkParseResult.Invalid };
		}
	}
}