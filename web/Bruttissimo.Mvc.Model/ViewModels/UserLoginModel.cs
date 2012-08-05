using Bruttissimo.Common;
using FluentValidation.Attributes;

namespace Bruttissimo.Mvc.Model
{
	[Validator(typeof (UserLoginModelValidator))]
	public class UserLoginModel
	{
		public string ReturnUrl { get; set; }
        public bool HasReturnUrl { get { return !ReturnUrl.NullOrEmpty(); } }

        public string Email { get; set; }
        public string Password { get; set; }

		public string OpenIdProvider { get; set; }

		public AuthenticationSource? Source { get; set; }

		/// <summary>
		/// OAuth-provided user Id on either Facebook Graph or Twitter platform
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// OAuth-provided access token required for Facebook Graph Api calls.
		/// </summary>
		public string AccessToken { get; set; }

		/// <summary>
		/// OAuth-provided user name from Twitter @Anywhere Api.
		/// </summary>
		public string DisplayName { get; set; }
	}

	public enum AuthenticationSource
	{
		Local,
		OpenId,
		Facebook,
		Twitter
	}
}