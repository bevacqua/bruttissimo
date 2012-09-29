using System;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Domain.Logic
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly PlainAuthenticationPortal plainAuthentication;
        private readonly OpenIdAuthenticationPortal openIdAuthentication;
        private readonly OAuthAuthenticationPortal oAuthAuthentication;

        public AuthenticationService(PlainAuthenticationPortal plainAuthentication, OpenIdAuthenticationPortal openIdAuthentication, OAuthAuthenticationPortal oAuthAuthentication)
        {
            Ensure.That(plainAuthentication, "plainAuthentication").IsNotNull();
            Ensure.That(openIdAuthentication, "openIdAuthentication").IsNotNull();
            Ensure.That(oAuthAuthentication, "oAuthAuthentication").IsNotNull();

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
