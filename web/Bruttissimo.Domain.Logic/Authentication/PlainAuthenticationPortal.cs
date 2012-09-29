using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Mvc.InversionOfControl.Membership;
using Bruttissimo.Domain.Authentication;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.Authentication
{
    public class PlainAuthenticationPortal : AuthenticationPortal
    {
        private readonly IUserService userService;
        private readonly IMembershipProvider membershipProvider;

        public PlainAuthenticationPortal(IUserService userService, IMembershipProvider membershipProvider, IFormsAuthentication formsAuthentication)
            : base(userService, formsAuthentication)
        {
            Ensure.That(userService, "userService").IsNotNull();
            Ensure.That(membershipProvider, "membershipProvider").IsNotNull();

            this.userService = userService;
            this.membershipProvider = membershipProvider;
        }

        internal AuthenticationResult Authenticate(string email, string password)
        {
            Ensure.That(email, "email").IsNotNull();
            Ensure.That(password, "password").IsNotNull();

            bool isNewUser = false;
            User user = userService.GetByEmail(email);
            if (user == null) // create a brand new account.
            {
                user = userService.CreateWithCredentials(email, password);
                isNewUser = true;
            }
            else // attempt to authenticate existing user.
            {
                bool valid = membershipProvider.ValidateUser(email, password);
                if (!valid)
                {
                    return InvalidAuthentication();
                }
            }
            return SuccessfulAuthentication(user, isNewUser);
        }
    }
}
