using System;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class PlainAuthenticationPortal : AuthenticationPortal
    {
        private readonly IUserService userService;
        private readonly IMembershipProvider membershipProvider;

        public PlainAuthenticationPortal(IUserService userService, IMembershipProvider membershipProvider, IFormsAuthentication formsAuthentication)
            : base(userService, formsAuthentication)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            if (membershipProvider == null)
            {
                throw new ArgumentNullException("membershipProvider");
            }
            this.userService = userService;
            this.membershipProvider = membershipProvider;
        }

        internal AuthenticationResult Authenticate(string email, string password)
        {
            if (email == null)
            {
                throw new ArgumentNullException("email");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            User user = userService.GetByEmail(email);
            bool isNewUser = false;
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
