using System.Web.Mvc;
using Bruttissimo.Common.Mvc.Core.Controllers;
using MvcSiteMapProvider.Web;

namespace Bruttissimo.Mvc.Controller.Controllers
{
    public class HomeController : ExtendedController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SiteMapXml()
        {
            return new XmlSiteMapResult();
        } 
    }
}
