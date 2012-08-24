using System;

namespace Bruttissimo.Domain.Logic
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly PlainAuthenticationPortal plainAuthentication;
        private readonly OpenIdAuthenticationPortal openIdAuthentication;
        private readonly OAuthAuthenticationPortal oAuthAuthentication;

        public AuthenticationService(PlainAuthenticationPortal plainAuthentication, OpenIdAuthenticationPortal openIdAuthentication, OAuthAuthenticationPortal oAuthAuthentication)
        {
            if (plainAuthentication == null)
            {
                throw new ArgumentNullException("plainAuthentication");
            }
            if (openIdAuthentication == null)
            {
                throw new ArgumentNullException("openIdAuthentication");
            }
            if (oAuthAuthentication == null)
            {
                throw new ArgumentNullException("oAuthAuthentication");
            }
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
