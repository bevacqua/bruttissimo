using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using log4net;

namespace Bruttissimo.Mvc.Controller
{
	public class TestController : ExtendedController
	{
		private readonly ILog log = LogManager.GetLogger(typeof(TestController));

		public ActionResult EmitSignal()
		{
			log.Debug("HOLA DEL INFRAMUNDO MAN! jouejoue");
			return new EmptyResult();
		}
	}
}
