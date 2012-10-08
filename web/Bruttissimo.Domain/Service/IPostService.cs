using System.Collections.Generic;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface IPostService
    {
        Post Create(Link link, string message, User user);
        Post GetById(long id, bool includeLink = false, bool includeComments = false);
        IEnumerable<Post> GetLatest(long? timestamp, int count);
        string GetTitleSlug(Post post);
        string BeautifyUserMessage(string message);
    }
}
