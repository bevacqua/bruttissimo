using Bruttissimo.Common;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Mvc.InversionOfControl.Membership;
using Bruttissimo.Common.Resources;
using Bruttissimo.Domain.Authentication;
using Bruttissimo.Domain.Service;
using Facebook;
using log4net;
using User = Bruttissimo.Domain.Entity.Entities.User;

namespace Bruttissimo.Domain.Logic.Authentication
{
    public class OAuthAuthenticationPortal : AuthenticationPortal
    {
        private readonly ILog log = LogManager.GetLogger(typeof (OAuthAuthenticationPortal));
        private readonly IUserService userService;

        public OAuthAuthenticationPortal(IUserService userService, IFormsAuthentication formsAuthentication)
            : base(userService, formsAuthentication)
        {
            Ensure.That(userService, "userService").IsNotNull();

            this.userService = userService;
        }

        internal AuthenticationResult AuthenticateWithFacebook(string facebookId, string accessToken)
        {
            Ensure.That(facebookId, "facebookId").IsNotNull();
            Ensure.That(accessToken, "accessToken").IsNotNull();

            dynamic response;
            try
            {
                FacebookClient client = new FacebookClient(accessToken); // TODO: move to FacebookProvider.
                response = client.Get("me");
            }
            catch (FacebookApiException exception)
            {
                log.Info(Error.FacebookApiException, exception);
                return AuthenticationException(exception);
            }
            if (response == null) // sanity
            {
                return AbortedAuthentication(Error.FacebookApiException);
            }
            if (response.error != null)
            {
                return AbortedAuthentication(response.error.message ?? Error.FacebookApiException);
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
            Ensure.That(twitterId, "twitterId").IsNotNull();
            Ensure.That(displayName, "displayName").IsNotNull();

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
