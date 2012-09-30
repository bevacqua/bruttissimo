using System;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Core.Controllers;
using Bruttissimo.Common.Static;
using Bruttissimo.Domain.Service;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Controller.Controllers
{
    public class AnalyticsController : ExtendedController
    {
        private readonly IGoogleAnalyticsService analyticsService;

        public AnalyticsController(IGoogleAnalyticsService analyticsService)
        {
            Ensure.That(() => analyticsService).IsNotNull();

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
