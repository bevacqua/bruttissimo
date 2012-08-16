using System;
using System.Security.Principal;
using System.Web.Security;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Domain.Logic.Email.Model;

namespace Bruttissimo.Domain.Logic
{
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;
		private readonly IEmailService emailService;

		public UserService(IUserRepository userRepository, IEmailService emailService)
		{
			if (userRepository == null)
			{
				throw new ArgumentNullException("userRepository");
			}
			if (emailService == null)
			{
				throw new ArgumentNullException("emailService");
			}
			this.userRepository = userRepository;
			this.emailService = emailService;
		}

		public User GetById(long id)
		{
			User user = userRepository.GetById(id);
			return user;
		}

		public User GetByEmail(string email)
		{
			if (email == null)
			{
				throw new ArgumentNullException("email");
			}
			User user = userRepository.GetByEmail(email);
			return user;
		}

		public User GetByOpenId(string openId)
		{
			User user = userRepository.GetByOpenId(openId);
			return user;
		}

		public User GetByFacebookGraphId(string facebookId)
		{
			User user = userRepository.GetByFacebookGraphId(facebookId);
			return user;
		}

		public User GetByTwitterId(string twitterId)
		{
			User user = userRepository.GetByTwitterId(twitterId);
			return user;
		}

		public User CreateWithCredentials(string email, string password)
		{
			if (email.NullOrBlank())
			{
				throw new ArgumentNullException("email");
			}
			if (password.NullOrBlank())
			{
				throw new ArgumentNullException("password");
			}
			User user = userRepository.GetByEmail(email);
			if (user != null)
			{
				throw new MembershipCreateUserException(MembershipCreateStatus.DuplicateEmail);
			}
			user = userRepository.CreateWithCredentials(email, password);
			SendRegistrationEmail(user);
			return user;
		}

		public User CreateWithOpenId(string openId, string email, string displayName)
		{
			User user = userRepository.CreateWithOpenId(openId, email, displayName);
			return user;
		}

		public User CreateWithFacebook(string facebookId, string accessToken, string email, string displayName)
		{
			User user = userRepository.CreateWithFacebook(facebookId, accessToken, email, displayName);
			return user;
		}

		public User CreateWithTwitter(string twitterId, string displayName)
		{
			User user = userRepository.CreateWithTwitter(twitterId, displayName);
			return user;
		}

		public void AddOpenIdConnection(User user, string openId)
		{
			userRepository.AddOpenIdConnection(user, openId);
		}

		public void AddFacebookConnection(User user, string facebookId, string accessToken)
		{
			userRepository.AddFacebookConnection(user, facebookId, accessToken);
		}

		public void SendRegistrationEmail(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			emailService.SendRegistrationEmail(user.Email, new RegistrationEmailModel
			{
				DisplayName = user.DisplayName
			});
		}

		public string GetAuthCookie(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			string encoded = EncodeUserIdIntoAuthCookie(user.Id);
			return encoded;
		}

		public long? GetUserId(IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			MiniIdentity mini = identity as MiniIdentity;
			if (mini != null)
			{
				return mini.User.Id;
			}
			else if (identity.IsAuthenticated)
			{
				long id = DecodeUserIdFromAuthCookie(identity.Name);
				return id;
			}
			return null;
		}

		public bool IsInRoleOrHasRight(User user, string roleOrRight)
		{
			bool allowed = userRepository.IsInRoleOrHasRight(user, roleOrRight);
			return allowed;
		}

		private string EncodeUserIdIntoAuthCookie(long id)
		{
			string encoded = Common.Resources.Authentication.AuthCookiePrefix + id;
			return encoded;
		}

		private long DecodeUserIdFromAuthCookie(string authCookie)
		{
			string decoded = authCookie.Replace(Common.Resources.Authentication.AuthCookiePrefix, string.Empty);
			long id = long.Parse(decoded);
			return id;
		}
	}
}