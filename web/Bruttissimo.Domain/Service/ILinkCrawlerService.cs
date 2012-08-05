using System;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
	public interface ILinkCrawlerService
	{
		Link CrawlHttpResource(Uri uri);
	}
}