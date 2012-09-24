using System;
using System.Text;
using System.Web;
using Bruttissimo.Common;
using Bruttissimo.Domain;

namespace Bruttissimo.Domain.Logic
{
    public sealed class GoogleAnalyticsService : IGoogleAnalyticsService
    {
        /// <summary>
        /// Gets the URL to the __utm.gif (Google Analytics Tracker)
        /// </summary>
        /// <param name="analyticsID">Google Analytics ID (UA-xxxxxx-x)</param>
        /// <param name="domain">Domain to track <example>sub.domain.com</example></param>
        /// <param name="referer">Refering page</param>
        /// <param name="title">Name of page (shown in the Google Analytics Dashboard)</param>
        /// <param name="data">User defined data to pass to Google Analytics</param>
        public string BuildPixelUrl(string analyticsID, string domain, string referer, string title, string data)
        {
            int requestId = new Random().Next(100000000, 999999999);
            int cookieId = new Random().Next(100000000, 999999999);
            int random = new Random().Next(1000000000, 2147483647);
            long timestamp = DateTime.UtcNow.ToUnixTime();

            // reference: http://code.google.com/apis/analytics/docs/tracking/gaTrackingTroubleshooting.html

            StringBuilder builder = new StringBuilder("http://www.google-analytics.com/__utm.gif");

            builder.Append("?utmwv=1");
            builder.AppendFormat("&utmn={0}", requestId);
            builder.Append("&utmsr=-");
            builder.Append("&utmsc=-");
            builder.Append("&utmul=-");
            builder.Append("&utmje=0");
            builder.Append("&utmfl=-");
            builder.Append("&utmdt=-");
            builder.AppendFormat("&utmhn={0}", EncodedOrDefault(domain));
            builder.AppendFormat("&utmr={0}", EncodedOrDefault(referer));
            builder.AppendFormat("&utmp={0}", EncodedOrDefault(title, "noscript"));
            builder.AppendFormat("&utmac={0}", analyticsID);
            builder.AppendFormat("&utmcc=__utma%3D{0}.{1}.{2}.{2}.{2}.2", cookieId, random, timestamp);
            builder.AppendFormat("%3B%2B__utmb%3D{0}", cookieId);
            builder.AppendFormat("%3B%2B__utmc%3D{0}", cookieId);
            builder.AppendFormat("%3B%2B__utmz%3D{0}.{1}.2.2", cookieId, timestamp);
            builder.AppendFormat(".utmccn%3D({0}))%7Cutmcsr%3D({0})", referer.NullOrBlank() ? "direct" : "referral");
            builder.Append("%7Cutmcmd%3D(none)");
            builder.AppendFormat("%3B%2B__utmv%3D{0}.{1}%3B", cookieId, EncodedOrDefault(data));

            string pixel = builder.ToString();
            return pixel;
        }

        private string EncodedOrDefault(string text, string defaultText = "-")
        {
            if (text.NullOrBlank())
            {
                return defaultText;
            }
            else
            {
                return HttpUtility.UrlEncode(text);
            }
        }
    }
}