using System;
using System.Web.Mvc;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
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
            Ensure.That(analyticsService, "analyticsService").IsNotNull();

            this.analyticsService = analyticsService;
        }

        [ChildActionOnly]
        public ActionResult Pixel(string title)
        {
            if (!Config.Site.Analytics)
            {
                return new EmptyResult();
            }
            Uri domain = new Uri(Config.Site.Home);

            string analyticsId = Config.Site.AnalyticsId;
            string host = domain.Host;
            string referer = Request.ServerVariables["HTTP_REFERER"];
            string absolute = Request.Url.AbsolutePath;
            string user = User == null ? null : User.Identity.Name;
            string pixel = analyticsService.BuildPixelUrl(analyticsId, host, referer, absolute, title, user);

            AnalyticsPixelModel model = new AnalyticsPixelModel
            {
                Account = analyticsId,
                Pixel = pixel
            };
            return PartialView(model); // also registers the JavaScript version that will be used most of the time.
        }
    }
}
