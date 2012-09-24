using System;
using System.Web.Mvc;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Mvc.Model;

namespace Bruttissimo.Mvc.Controller
{
    public class AnalyticsController : ExtendedController
    {
        private readonly IGoogleAnalyticsService analyticsService;

        public AnalyticsController(IGoogleAnalyticsService analyticsService)
        {
            if (analyticsService == null)
            {
                throw new ArgumentNullException("analyticsService");
            }
            this.analyticsService = analyticsService;
        }

        [ChildActionOnly]
        public ActionResult Pixel(string title)
        {
            if (!Config.Site.Analytics)
            {
                return new EmptyResult();
            }
            string analyticsId = Config.Site.AnalyticsId;
            string domain = Config.Site.Home;
            string referer = Request.ServerVariables["HTTP_REFERER"];
            string user = User == null ? null : User.Identity.Name;
            string pixel = analyticsService.BuildPixelUrl(analyticsId, domain, referer, title, user);

            AnalyticsPixelModel model = new AnalyticsPixelModel
            {
                Account = analyticsId,
                Pixel = pixel
            };
            return PartialView(model); // also registers the JavaScript version that will be used most of the time.
        }
    }
}
