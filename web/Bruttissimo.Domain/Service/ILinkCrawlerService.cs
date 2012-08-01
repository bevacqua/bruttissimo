using System;

namespace Bruttissimo.Domain.Logic
{
	public interface ILinkCrawlerService
	{
		Link CrawlHttpResource(Uri uri);
	}
}