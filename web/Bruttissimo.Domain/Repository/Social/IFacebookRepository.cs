using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IFacebookRepository
    {
        IList<FacebookPost> GetPostsInGroupFeed(string group, DateTime? since);
        IList<FacebookPost> GetPostsInFeed(string url, DateTime? since);
    }
}