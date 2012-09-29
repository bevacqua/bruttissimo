using System;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface ILinkCrawlerService
    {
        Link CrawlHttpResource(Uri uri);
    }
}
