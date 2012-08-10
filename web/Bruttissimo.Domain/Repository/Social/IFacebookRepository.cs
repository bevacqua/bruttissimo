using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IFacebookRepository
    {
        IList<FacebookPost> GetPostsInGroupFeed(string group, out string next);
        IList<FacebookPost> GetPostsInFeed(string url, out string next);
    }
}