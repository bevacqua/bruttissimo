using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
	public interface IPostService
	{
		Post GetById(long id, bool includeLink = true);
		IEnumerable<Post> GetLatest(long? timestamp, int count);
		string GetTitleSlug(Post post);
		Post Create(Link link, string message, User user);
	}
}