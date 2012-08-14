using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IFacebookRepository
    {
        IEnumerable<FacebookPost> GetPostsInGroupFeed(string group, DateTime? since);
        IEnumerable<FacebookPost> GetPostsInFeed(string url, DateTime? since);
    }
}