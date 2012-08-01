using System;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Bruttissimo.Domain.Logic
{
	public class OpenIdAuthenticationPortal : AuthenticationPortal
	{
		private readonly OpenIdRelyingParty relyingParty;
		private readonly IUserService userService;

		public OpenIdAuthenticationPortal(OpenIdRelyingParty relyingParty, IUserService userService, IFormsAuthentication formsAuthentication)
			: base(userService, formsAuthentication)
		{
			if (relyingParty == null)
			{
				throw new ArgumentNullException("relyingParty");
			}
			if (userService == null)
			{
				throw new ArgumentNullException("userService");
			}
			this.relyingParty = relyingParty;
			this.userService = userService;
		}

		internal AuthenticationResult Authenticate(string providerUrl, Uri returnUrl)
		{
			IAuthenticationResponse response = relyingParty.GetResponse();
			if (response == null) // Client submitting Identifier.
			{
				return PrepareOpenIdRequest(providerUrl, returnUrl);
			}
			else // OpenId provider sending assertion response.
			{
				return ProcessOpenIdResponse(response);
			}
		}

		internal AuthenticationResult PrepareOpenIdRequest(string providerUrl, Uri returnUrl)
		{
			Identifier identifier;
			if (Identifier.TryParse(providerUrl, out identifier))
			{
				try
				{
					IAuthenticationRequest request = relyingParty.CreateRequest(identifier, Realm.AutoDetect, returnUrl);

					FetchRequest fetch = new FetchRequest(); // request the authentication provider for some additional information.
					fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
					fetch.Attributes.AddRequired(WellKnownAttributes.Name.FullName);
					request.AddExtension(fetch);

					return new AuthenticationResult
					{
						Status = ConnectionStatus.RedirectToProvider,
						Action = request.RedirectingResponse.AsActionResult()
					};
				}
				catch (ProtocolException exception)
				{
					return AuthenticationException(exception);
				}
			}
			return AbortedAuthentication(Common.Resources.Authentication.InvalidOpenIdIdentifier);
		}

		internal AuthenticationResult ProcessOpenIdResponse(IAuthenticationResponse response)
		{
			switch (response.Status)
			{
				case AuthenticationStatus.Canceled:
				{
					return AbortedAuthentication(Common.Resources.Authentication.CanceledAtProvider, ConnectionStatus.Canceled);
				}
				case AuthenticationStatus.Failed:
				{
					return AuthenticationException(response.Exception);
				}
				case AuthenticationStatus.Authenticated:
				{
					string openId = response.ClaimedIdentifier;
					User user = userService.GetByOpenId(openId);
					bool isNewUser = false;
					bool isNewConnection = false;
					if (user == null)
					{
						isNewConnection = true;

						string email = null;
						string displayName = null;
						var fetch = response.GetExtension<FetchResponse>();
						if (fetch != null)
						{
							email = fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);
							displayName = fetch.GetAttributeValue(WellKnownAttributes.Name.FullName);
						}
						if (!email.NullOrEmpty()) // maybe they already have an account.
						{
							user = userService.GetByEmail(email);
						}
						if (user == null) // create a brand new account.
						{
							user = userService.CreateWithOpenId(openId, email, displayName);
							isNewUser = true;
						}
						else // just connect the existing account to their OpenId account.
						{
							userService.AddOpenIdConnection(user, openId);
						}
					}
					return SuccessfulAuthentication(user, isNewUser, isNewConnection);
				}
			}
			return AbortedAuthentication(Common.Resources.Authentication.InvalidOpenAuthenticationStatus);
		}
	}
}