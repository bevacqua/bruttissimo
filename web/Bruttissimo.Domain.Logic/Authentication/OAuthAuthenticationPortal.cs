using System;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;
using Facebook;
using log4net;

namespace Bruttissimo.Domain.Logic
{
    public class OAuthAuthenticationPortal : AuthenticationPortal
    {
        private readonly ILog log = LogManager.GetLogger(typeof (OAuthAuthenticationPortal));
        private readonly IUserService userService;

        public OAuthAuthenticationPortal(IUserService userService, IFormsAuthentication formsAuthentication)
            : base(userService, formsAuthentication)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            this.userService = userService;
        }

        internal AuthenticationResult AuthenticateWithFacebook(string facebookId, string accessToken)
        {
            if (facebookId == null)
            {
                throw new ArgumentNullException("facebookId");
            }
            if (accessToken == null)
            {
                throw new ArgumentNullException("accessToken");
            }
            dynamic response;
            try
            {
                FacebookClient client = new FacebookClient(accessToken); // TODO: move to FacebookProvider.
                response = client.Get("me");
            }
            catch (FacebookApiException exception)
            {
                log.Info(Common.Resources.Error.FacebookApiException, exception);
                return AuthenticationException(exception);
            }
            if (response == null) // sanity
            {
                return AbortedAuthentication(Common.Resources.Error.FacebookApiException);
            }
            if (response.error != null)
            {
                return AbortedAuthentication(response.error.message ?? Common.Resources.Error.FacebookApiException);
            }
            if (response.id != facebookId) // validate access token against facebookId for enhanced security.
            {
                return InvalidAuthentication();
            }
            User user = userService.GetByFacebookGraphId(facebookId);
            bool isNewUser = false;
            bool isNewConnection = false;
            if (user == null)
            {
                isNewConnection = true;

                string email = response.email;
                if (!email.NullOrEmpty()) // maybe they already have an account.
                {
                    user = userService.GetByEmail(email);
                }
                if (user == null) // create a brand new account.
                {
                    string displayName = response.name;
                    user = userService.CreateWithFacebook(facebookId, accessToken, email, displayName);
                    isNewUser = true;
                }
                else // just connect the existing account to their Facebook account.
                {
                    userService.AddFacebookConnection(user, facebookId, accessToken);
                }
            }
            return SuccessfulAuthentication(user, isNewUser, isNewConnection);
        }

        internal AuthenticationResult AuthenticateWithTwitter(string twitterId, string displayName)
        {
            if (twitterId == null)
            {
                throw new ArgumentNullException("twitterId");
            }
            if (displayName == null)
            {
                throw new ArgumentNullException("displayName");
            }
            User user = userService.GetByTwitterId(twitterId);
            bool isNewUser = false;
            if (user == null) // create a brand new account.
            {
                user = userService.CreateWithTwitter(twitterId, displayName);
                isNewUser = true;
            }
            return SuccessfulAuthentication(user, isNewUser);
        }
    }
}
