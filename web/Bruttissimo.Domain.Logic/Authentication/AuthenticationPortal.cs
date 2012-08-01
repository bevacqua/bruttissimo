using System;
using Bruttissimo.Common.Mvc;

namespace Bruttissimo.Domain.Logic
{
	public abstract class AuthenticationPortal
	{
		private readonly IUserService userService;
		private readonly IFormsAuthentication formsAuthentication;

		protected AuthenticationPortal(IUserService userService, IFormsAuthentication formsAuthentication)
		{
			if (userService == null)
			{
				throw new ArgumentNullException("userService");
			}
			if (formsAuthentication == null)
			{
				throw new ArgumentNullException("formsAuthentication");
			}
			this.userService = userService;
			this.formsAuthentication = formsAuthentication;
		}

		internal protected AuthenticationResult InvalidAuthentication()
		{
			return new AuthenticationResult
			{
				Status = ConnectionStatus.InvalidCredentials,
				Message = Common.Resources.Authentication.InvalidCredentials
			};
		}

		internal protected AuthenticationResult AbortedAuthentication(string message, ConnectionStatus status = ConnectionStatus.Faulted)
		{
			return new AuthenticationResult
			{
				Status = status,
				Message = message,
			};
		}

		internal protected AuthenticationResult AuthenticationException(Exception exception)
		{
			return new AuthenticationResult
			{
				Status = ConnectionStatus.Faulted,
				Message = exception.Message,
				Exception = exception
			};
		}

		internal protected AuthenticationResult SuccessfulAuthentication(User user, bool isNewUser = false, bool? isNewConnection = null)
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