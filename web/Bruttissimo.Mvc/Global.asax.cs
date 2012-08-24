using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common;
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
            System.Diagnostics.Debugger.Break(); // debug application start in IIS.
            CompositionRoot.Install(new ApplicationInstaller());

            AreaRegistration.RegisterAllAreas();
            Routing.RegisterRoutes(RouteTable.Routes);

            IJobAutoRunner autoRunner = IoC.Container.Resolve<IJobAutoRunner>();
            autoRunner.Fire(); // fire auto run jobs.

            log.Debug(Debug.ApplicationStart);
        }

        public override void Init()
        {
            base.Init();

            IHttpModule[] modules = IoC.Container.ResolveAll<IHttpModule>();

            foreach (IHttpModule module in modules)
            {
                module.Init(this);
            }
        }

        protected void Application_End()
        {
            log.Debug(Debug.ApplicationEnd);
            IoC.Shutdown();
        }
    }
}
