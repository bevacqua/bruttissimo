using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Repository
{
    public interface IPostRepository : IEntityRepository<Post>
    {
        Post GetById(long postId, bool includeLink);
        IEnumerable<Post> GetLatest(DateTime? until, int count);
        Post Insert(Link link, string message, User user);
        IEnumerable<Post> GetPostsPendingFacebookExport();
        IEnumerable<Post> GetPostsPendingTwitterExport();
    }
}
