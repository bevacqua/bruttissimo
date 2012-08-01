using System.Web.Mvc;
using Bruttissimo.Common.Mvc;

namespace Bruttissimo.Mvc.Controllers
{
	public class HomeController : ExtendedController
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
