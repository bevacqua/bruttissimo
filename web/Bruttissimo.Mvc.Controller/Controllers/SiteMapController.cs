using System.Web.Mvc;
using Bruttissimo.Common.Mvc.Core.Attributes;
using Bruttissimo.Common.Mvc.Core.Controllers;
using MvcSiteMapProvider.Web;

namespace Bruttissimo.Mvc.Controller.Controllers
{
    public class SiteMapController : ExtendedController
    {
        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView();
        }

        [NotAjax]
        public ActionResult Xml()
        {
            return new XmlSiteMapResult();
        }
    }
}