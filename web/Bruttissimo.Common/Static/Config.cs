﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using Bruttissimo.Common.Resources;

namespace Bruttissimo.Common.Static
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
            return default(bool?);
        }

        internal static int? Int(string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return default(int?);
        }

        internal static double? Double(string value)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            return default(double?);
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

            public static string TwitterAccessToken
            {
                get { return Get("Social.TwitterAccessToken"); }
            }

            public static string TwitterAccessTokenSecret
            {
                get { return Get("Social.TwitterAccessTokenSecret"); }
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
            public static bool Analytics
            {
                get { return Boolean(Get("Site.Analytics")) ?? false; }
            }

            public static string AnalyticsId
            {
                get { return Get("Site.AnalyticsId"); }
            }

            public static string Home
            {
                get { return Get("Site.Home"); }
            }

            public static string Pixel
            {
                get { return Get("Site.Pixel"); }
            }

            public static int? Port
            {
                get { return Int(Get("Site.Port")); }
            }
        }

        public static class Defaults
        {
            public static double TimeZone
            {
                get { return Double(Get("Defaults.TimeZone")) ?? 0; }
            }

            public static int PostListPageSize
            {
                get { return Int(Get("Defaults.PostListPageSize")) ?? 1; }
            }
        }

        public static IList<KeyValuePair<string, string>> AsKeyValuePairs()
        {
            NameValueCollection set = ConfigurationManager.AppSettings;
            string[] keys = set.AllKeys;
            return keys.Select(key => new KeyValuePair<string, string>(key, set[key])).ToList();
        }
    }
}