using System;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Mvc.InversionOfControl.Membership;
using Bruttissimo.Domain.Authentication;
using Bruttissimo.Domain.Service;
using User = Bruttissimo.Domain.Entity.Entities.User;

namespace Bruttissimo.Domain.Logic.Authentication
{
    public abstract class AuthenticationPortal
    {
        private readonly IUserService userService;
        private readonly IFormsAuthentication formsAuthentication;

        protected AuthenticationPortal(IUserService userService, IFormsAuthentication formsAuthentication)
        {
            Ensure.That(() => userService).IsNotNull();
            Ensure.That(() => formsAuthentication).IsNotNull();

            this.userService = userService;
            this.formsAuthentication = formsAuthentication;
        }

        protected internal AuthenticationResult InvalidAuthentication()
        {
            return new AuthenticationResult
            {
                Status = ConnectionStatus.InvalidCredentials,
                Message = Common.Resources.Authentication.InvalidCredentials
            };
        }

        protected internal AuthenticationResult AbortedAuthentication(string message, ConnectionStatus status = ConnectionStatus.Faulted)
        {
            return new AuthenticationResult
            {
                Status = status,
                Message = message,
            };
        }

        protected internal AuthenticationResult AuthenticationException(Exception exception)
        {
            return new AuthenticationResult
            {
                Status = ConnectionStatus.Faulted,
                Message = exception.Message,
                Exception = exception
            };
        }

        protected internal AuthenticationResult SuccessfulAuthentication(User user, bool isNewUser = false, bool? isNewConnection = null)
        {
            string authCookie = userService.GetAuthCookie(user);
            formsAuthentication.SetAuthCookie(authCookie, true);

            return new AuthenticationResult
            {
                Status = ConnectionStatus.Authenticated,
                DisplayName = user.DisplayName,
                UserId = user.Id,
                IsNewUser = isNewUser,
                IsNewConnection = isNewConnection ?? isNewUser
            };
        }
    }
}
