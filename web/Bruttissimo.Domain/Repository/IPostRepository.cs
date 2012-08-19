using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IPostRepository : IEntityRepository<Post>
    {
        Post GetById(long postId, bool includeLink);
        IEnumerable<Post> GetLatest(DateTime? until, int count);
        Post Insert(Link link, string message, User user);
    }
}
