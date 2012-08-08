using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Logic.Hubs;
using SignalR;
using SignalR.Hubs;

namespace Bruttissimo.Mvc.Controller
{
	public class TestController : ExtendedController
	{
		public ActionResult EmitSignal()
		{
			IHubContext logHub = GlobalHost.ConnectionManager.GetHubContext<LogHub>();
			logHub.Clients.testBcast("HOLA DEL INFRAMUNDO MAN! jouejoue");
			return new EmptyResult();
		}
	}
}
