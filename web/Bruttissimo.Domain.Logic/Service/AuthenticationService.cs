using System;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Authentication;
using Bruttissimo.Domain.Logic.Authentication;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly PlainAuthenticationPortal plainAuthentication;
        private readonly OpenIdAuthenticationPortal openIdAuthentication;
        private readonly OAuthAuthenticationPortal oAuthAuthentication;

        public AuthenticationService(PlainAuthenticationPortal plainAuthentication, OpenIdAuthenticationPortal openIdAuthentication, OAuthAuthenticationPortal oAuthAuthentication)
        {
            Ensure.That(() => plainAuthentication).IsNotNull();
            Ensure.That(() => openIdAuthentication).IsNotNull();
            Ensure.That(() => oAuthAuthentication).IsNotNull();

            this.plainAuthentication = plainAuthentication;
            this.openIdAuthentication = openIdAuthentication;
            this.oAuthAuthentication = oAuthAuthentication;
        }

        public AuthenticationResult AuthenticateWithCredentials(string email, string password)
        {
            return plainAuthentication.Authenticate(email, password);
        }

        public AuthenticationResult AuthenticateWithFacebook(string facebookId, string accessToken)
        {
            return oAuthAuthentication.AuthenticateWithFacebook(facebookId, accessToken);
        }

        public AuthenticationResult AuthenticateWithTwitter(string twitterId, string displayName)
        {
            return oAuthAuthentication.AuthenticateWithTwitter(twitterId, displayName);
        }

        public AuthenticationResult AuthenticateWithOpenId(string providerUrl, Uri returnUrl)
        {
            return openIdAuthentication.Authenticate(providerUrl, returnUrl);
        }
    }
}
