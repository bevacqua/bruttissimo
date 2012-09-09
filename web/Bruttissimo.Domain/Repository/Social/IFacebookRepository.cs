using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IFacebookRepository
    {
        IList<FacebookPost> GetPostsInFeed(FacebookImportOptions opts);
        FacebookPost PostToFeed(Post post, string userAccessToken);
    }
}
