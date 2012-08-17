using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using Bruttissimo.Common.Resources;

namespace Bruttissimo.Common
{
	public static class Config
	{
		#region Common

		public static string Get(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		public static string GetConnectionString(string key)
		{
			return ConfigurationManager.ConnectionStrings[key].ConnectionString;
		}

		internal static bool? Boolean(string value)
		{
			bool result;
			if (bool.TryParse(value, out result))
			{
				return result;
			}
			return null;
		}

		internal static int? Int(string value)
		{
			int result;
			if (int.TryParse(value, out result))
			{
				return result;
			}
			return null;
		}

		#endregion

		public static class Debug
		{
			public static bool RequestLog
			{
				get { return Boolean(Get("Debug.RequestLog")) ?? false; }
			}

			public static bool IgnoreMinification
			{
				get { return Boolean(Get("Debug.IgnoreMinification")) ?? false; }
			}
		}

		public static class Social
		{
			public static string FacebookAppId
			{
				get { return Get("Social.FacebookAppId"); }
			}
			public static string FacebookAppSecret
			{
				get { return Get("Social.FacebookAppSecret"); }
			}
			public static string FacebookAccessToken
			{
				get { return Get("Social.FacebookAccessToken"); }
			}
			public static string FacebookFeedId
			{
				get { return Get("Social.FacebookFeedId"); }
			}

			public static string TwitterAppId
			{
				get { return Get("Social.TwitterAppId"); }
			}
			public static string TwitterAppSecret
			{
				get { return Get("Social.TwitterAppSecret"); }
			}
		}

		public static class OutgoingEmail
		{
			public static int? Timeout
			{
				get { return Int(Get("OutgoingEmail.Timeout")); }
			}
			public static string Address
			{
				get { return Get("OutgoingEmail.Address"); }
			}
			public static string DisplayName
			{
				get { return Get("OutgoingEmail.DisplayName"); }
			}
			public static MailAddress GetAddress()
			{
				string address = Address;
				if (address == null)
				{
					SmtpClient client = new SmtpClient();
					NetworkCredential credentials = client.Credentials as NetworkCredential;
					if (credentials == null || credentials.UserName.IndexOf('@') < 0)
					{
						throw new InvalidCredentialException(Error.MissingEmailCredentials);
					}
					address = credentials.UserName;
				}
				string displayName = DisplayName ?? address;
				return new MailAddress(address, displayName);
			}
		}

		public static class Site
		{
			public static string Home
			{
				get { return Get("Site.Home"); }
			}
		}
	}
}
