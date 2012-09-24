using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Domain.Logic;
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
        public ActionResult Pixel()
        {
            if (!Config.Site.Analytics)
            {
                return new EmptyResult();
            }
            string analyticsId = Config.Site.AnalyticsId;
            string domain = null;
            string referer = null;
            string title = null;
            string user = "";// helper.ViewContext.HttpContext.User == null ? "" : "";
            string pixel = analyticsService.BuildPixelUrl(analyticsId, domain, referer, title, user);
            
            AnalyticsPixelModel model = new AnalyticsPixelModel
            {
                Account = analyticsId,
                Pixel = pixel
            };
            return View(model); // also registers the JavaScript version that will be used most of the time.
        }
    }
}
