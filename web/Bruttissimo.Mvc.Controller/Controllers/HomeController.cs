using System.Web.Mvc;
using Bruttissimo.Common.Mvc.Core.Attributes;
using Bruttissimo.Common.Mvc.Core.Controllers;

namespace Bruttissimo.Mvc.Controller.Controllers
{
    public class HomeController : ExtendedController
    {
        [HttpGet]
        [NotAjax]
        public ActionResult Index()
        {
            return View();
        }
    }
}
