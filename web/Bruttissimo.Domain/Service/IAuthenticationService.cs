using System;
using Bruttissimo.Domain.Authentication;

namespace Bruttissimo.Domain.Service
{
    public interface IAuthenticationService
    {
        AuthenticationResult AuthenticateWithCredentials(string email, string password);
        AuthenticationResult AuthenticateWithFacebook(string facebookId, string accessToken);
        AuthenticationResult AuthenticateWithTwitter(string twitterId, string displayName);
        AuthenticationResult AuthenticateWithOpenId(string providerUrl, Uri returnUrl);
    }
}
