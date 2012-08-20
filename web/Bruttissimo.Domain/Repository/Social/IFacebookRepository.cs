using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IFacebookRepository
    {
        IList<FacebookPost> GetPostsInFeed(string feed, DateTime? since, FacebookImportLog importLog);
    }
}
