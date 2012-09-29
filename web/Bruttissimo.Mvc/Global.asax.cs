using System.Web;
using System.Web.Routing;
using Bruttissimo.Common;
using Bruttissimo.Common.InversionOfControl;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Common.Resources;
using Bruttissimo.Mvc.Windsor;
using log4net;

namespace Bruttissimo.Mvc
{
    public class MvcApplication : HttpApplication
    {
        private readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            // System.Diagnostics.Debugger.Break(); // debug application start in IIS.
            CompositionRoot.Initialize(new ApplicationInstaller());

            Routing.RegisterAllAreas();
            Routing.RegisterSignalR(RouteTable.Routes);
            Routing.RegisterRoutes(RouteTable.Routes);

            log.Debug(Debug.ApplicationStart);
        }

        public override void Init()
        {
            IApplicationModuleManager manager = IoC.Container.Resolve<IApplicationModuleManager>();
            manager.Execute(this);
        }

        protected void Application_End()
        {
            log.Debug(Debug.ApplicationEnd);
            IoC.Shutdown();
        }
    }
}
