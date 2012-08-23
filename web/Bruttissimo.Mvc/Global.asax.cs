using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common;
using Bruttissimo.Common.Resources;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Mvc.Windsor;
using log4net;
using StackExchange.Profiling;

namespace Bruttissimo.Mvc
{
    public class MvcApplication : HttpApplication
    {
        private readonly ILog log = LogManager.GetLogger(typeof (HttpApplication));

        protected void Application_Start()
        {
            System.Diagnostics.Debugger.Break(); // debug application start in IIS.
            CompositionRoot.Install(new ApplicationInstaller());
            
            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();
            Routing.RegisterRoutes(RouteTable.Routes);
            log.Debug(Debug.ApplicationStart);
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove(Constants.ServerResponseHeader);
        }

        protected void Application_BeginRequest()
        {
            if (Config.Debug.RequestLog)
            {
                log.DebugFormat(Debug.ApplicationRequest, Request.RawUrl);
            }
            RequestSanitizer sanitizer = IoC.Container.Resolve<RequestSanitizer>();
            if (sanitizer.ValidateUrl())
            {
                return;
            }
            MiniProfiler.Start();
        }

        protected void Application_PostAuthenticateRequest()
        {
            IMiniAuthentication miniAuthentication = IoC.Container.Resolve<IMiniAuthentication>();
            miniAuthentication.SetContextPrincipal();

            if (!Request.CanDisplayDebuggingDetails())
            {
                // abort profiling session if this isn't a local request and the user is not an administrator.
                MiniProfiler.Stop(discardResults: true);
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        protected void Application_Error()
        {
            ExceptionHelper exceptionHelper = IoC.Container.Resolve<ExceptionHelper>();
            HttpApplicationErrorHander errorHandler = new HttpApplicationErrorHander(this, exceptionHelper);
            errorHandler.HandleApplicationError();
        }

        protected void Application_End()
        {
            log.Debug(Debug.ApplicationEnd);
            IoC.Shutdown();
        }
    }
}
