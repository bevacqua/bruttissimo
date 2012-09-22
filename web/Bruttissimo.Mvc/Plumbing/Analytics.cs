using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;

namespace Bruttissimo.Mvc
{
    public static class AnalyticsHelpers
    {
        public static IHtmlString Analytics(this HtmlHelper helper, IJavaScriptHelper scriptManager)
        {
            if (!IsEnabled())
            {
                return MvcHtmlString.Empty;
            }
            string analytics = helper.Partial("_Analytics").ToHtmlString();
            // TODO: what happens if rendering in a partial, Guid chaos?
            // TODO: guess: should provide context and have scriptManager figure out how to register
            scriptManager.Register("_Analytics", analytics);
            return MvcHtmlString.Empty;
        }

        public static IHtmlString AnalyticsPixel(this HtmlHelper helper)
        {
            if (!IsEnabled())
            {
                return MvcHtmlString.Empty;
            }
            IHtmlString pixel = helper.Partial("_AnalyticsPixel");
            return pixel;
        }

        internal static bool IsEnabled()
        {
            return Config.Site.Analytics;
        }
    }
}
