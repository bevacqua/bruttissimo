using System.Web.Mvc;
using Bruttissimo.Common.Mvc.Core.Controllers;

namespace Bruttissimo.Mvc.Controller.Controllers
{
    public class HomeController : ExtendedController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
