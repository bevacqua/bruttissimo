using System;
using System.Security.Principal;
using System.Web;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Service
{
    public interface IUserService
    {
        User GetById(long id);
        User GetByEmail(string email);
        User GetByOpenId(string openId);
        User GetByFacebookGraphId(string facebookId);
        User GetByTwitterId(string twitterId);

        User CreateWithCredentials(string email, string password);
        User CreateWithOpenId(string openId, string email, string displayName);
        User CreateWithFacebook(string facebookId, string accessToken, string email, string displayName);
        User CreateWithTwitter(string twitterId, string displayName);

        void AddOpenIdConnection(User user, string openId);
        void AddFacebookConnection(User user, string facebookId, string accessToken);

        void SendRegistrationEmail(User user);
        string GetAuthCookie(User user);
        long? GetUserId(IIdentity identity);
        bool IsInRoleOrHasRight(User user, string roleOrRight);

        DateTime ToCurrentUserTimeZone(HttpContextBase httpContext, DateTime dateTime);
    }
}
