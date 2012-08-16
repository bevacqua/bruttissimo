using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
	public interface IPostRepository
	{
        Post GetByPostId(long postId, bool includeLink);
        Post GetByFacebookPostId(string facebookPostId);
		IEnumerable<Post> GetLatest(DateTime? until, int count);
		Post Create(Link link, string message, User user);
	}
}