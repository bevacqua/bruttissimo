using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common;
using Bruttissimo.Common.Resources;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Castle.Windsor;
using log4net;
using StackExchange.Profiling;

namespace Bruttissimo.Mvc
{
	public class MvcApplication : HttpApplication, IContainerAccessor
	{
		private readonly ILog log = LogManager.GetLogger(typeof(HttpApplication));
		private static IWindsorContainer _container;

		public IWindsorContainer Container
		{
			get { return _container; }
		}

		protected void Application_Start()
		{
			// System.Diagnostics.Debugger.Break(); // debug application start in IIS.
			MvcHandler.DisableMvcResponseHeader = true;
			AreaRegistration.RegisterAllAreas();
			Routing.RegisterRoutes(RouteTable.Routes);

			InitializeDependencies();
            log.Debug(Debug.ApplicationStart);
		}

		private void InitializeDependencies()
		{
			_container = new WindsorContainer();
			_container.Install(new ApplicationInstaller());
			IoC.Bootstrap(_container);
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
			RequestSanitizer sanitizer = _container.Resolve<RequestSanitizer>();
			if (sanitizer.ValidateUrl())
			{
				return;
			}
			MiniProfiler.Start();
		}

		protected void Application_PostAuthenticateRequest()
		{
			IMiniAuthentication miniAuthentication = _container.Resolve<IMiniAuthentication>();
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
			ExceptionHelper exceptionHelper = _container.Resolve<ExceptionHelper>();
			HttpApplicationErrorHander errorHandler = new HttpApplicationErrorHander(this, exceptionHelper);
			errorHandler.HandleApplicationError();
		}

		protected void Application_End()
		{
			log.Debug(Debug.ApplicationEnd);

			if (_container != null)
			{
				_container.Dispose();
				_container = null;
			}
		}
	}
}