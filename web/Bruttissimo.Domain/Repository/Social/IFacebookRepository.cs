using System.Collections.Generic;
using Bruttissimo.Domain.DTO.Facebook;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Entity.Social.Facebook;

namespace Bruttissimo.Domain.Repository.Social
{
    public interface IFacebookRepository
    {
        IList<FacebookPost> GetPostsInFeed(FacebookImportOptions opts);
        FacebookPost PostToFeed(Post post, string userAccessToken);
        bool ValidateToken(string accessToken);
    }
}
