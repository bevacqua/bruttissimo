using System;
using System.Text;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Extensions.RazorEngine
{
    public class TemplateUrlHelper
    {
        public string Content(string relativeUrl)
        {
            Ensure.That(relativeUrl, "relativeUrl").IsNotNull();
            
            if (relativeUrl.StartsWith("~"))
            {
                relativeUrl = relativeUrl.Remove(0, 1);
            }
            if (relativeUrl.StartsWith("/"))
            {
                relativeUrl = relativeUrl.Remove(0, 1);
            }

            string baseUrl = Config.Site.Home;
            StringBuilder builder = new StringBuilder(baseUrl);

            if (!baseUrl.EndsWith("/"))
            {
                builder.Append("/");
            }
            builder.Append(relativeUrl.ToLowerInvariant());
            return builder.ToString();
        }

        public string Home()
        {
            return Content(string.Empty);
        }
    }
}
