using System;
using System.Collections.Specialized;
using System.Web;

namespace Bruttissimo.Common.Mvc
{
    public class RequestSanitizer
    {
        private readonly HttpContextBase context;

        public RequestSanitizer(HttpContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.context = context;
        }

        /// <summary>
        /// Fixes the request Url, if required. Returns a value indicating whether a redirect is necessary.
        /// </summary>
        public bool ValidateUrl()
        {
            HttpRequestBase request = context.Request;
            HttpResponseBase response = context.Response;
            UriBuilder builder = new UriBuilder(request.Url) { Port = Config.Site.Port ?? request.Url.Port };
            Uri requestUrl = builder.Uri;

            string incoming = requestUrl.GetLeftPart(UriPartial.Path);
            string absolute = requestUrl.AbsolutePath;
            string decoded = HttpUtility.UrlDecode(absolute);
            string redirect;

            if (absolute != decoded)
            {
                // encoded characters such as %7D result in issues, so we better leave them untouched.
                string host = requestUrl.GetLeftPart(UriPartial.Authority);
                redirect = host.ToLowerInvariant() + absolute;
            }
            else
            {
                redirect = incoming.ToLowerInvariant(); // our urls are always in lower case.
            }

            // remove www. from the beginning of the url.
            redirect = CompiledRegex.WwwSubdomain.Replace(redirect, Resources.Shared.Regex.WwwSubdomainReplacement);

            if (absolute.Length > 1 && absolute.EndsWith("/"))
            {
                // remove '/' from the end of the url unless this is the only character in the url.
                redirect = redirect.Substring(0, redirect.Length - 1);
            }
            if (redirect != incoming)
            {
                // append the query string to the redirect result.
                NameValueCollection query = request.QueryString;
                string queryString = string.Empty;

                if (query.Count > 0)
                {
                    queryString = "?{0}".FormatWith(query.ToString());
                }
                response.RedirectPermanent(redirect + queryString);
                return true;
            }
            return false;
        }
    }
}
