using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Static;
using Bruttissimo.Common.Utility;
using Bruttissimo.Domain.DTO.Link;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.Service
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository linkRepository;
        private readonly ILinkCrawlerService linkCrawler;
        private readonly HttpHelper httpHelper;

        public LinkService(ILinkRepository linkRepository, ILinkCrawlerService linkCrawler, HttpHelper httpHelper)
        {
            Ensure.That(() => linkRepository).IsNotNull();
            Ensure.That(() => linkCrawler).IsNotNull();
            Ensure.That(() => httpHelper).IsNotNull();
            
            this.linkRepository = linkRepository;
            this.linkCrawler = linkCrawler;
            this.httpHelper = httpHelper;
        }

        /// <summary>
        /// Parses a user-input text, inserts the records on the persister for future use, and returns the first occurrence of a web resource link.
        /// </summary>
        public LinkResult ParseUserInput(string text)
        {
            Ensure.That(() => text).IsNotNull();

            IList<Uri> uris = GetReferenceUris(text);
            LinkResult result = GetExistingLinkOrCrawlResource(uris);
            return result;
        }

        /// <summary>
        /// Extracts uris from a text input and returns them sanitized.
        /// </summary>
        public IList<Uri> GetReferenceUris(string text)
        {
            Ensure.That(() => text).IsNotNull();

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
            Ensure.That(() => uris).IsNotNull();

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
                    return new LinkResult {Result = LinkParseResult.Valid, Link = link};
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
            return new LinkResult {Result = LinkParseResult.Invalid};
        }
    }
}
