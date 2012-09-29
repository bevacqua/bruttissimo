using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Resources;

namespace Bruttissimo.Common.Mvc.Core.Controllers
{
    /// <summary>
    /// Controller dedicated to handling error views.
    /// </summary>
    public class ErrorController : ExtendedController
    {
        /// <summary>
        /// Wrap the current request context around an error controller instance.
        /// </summary>
        public static ErrorController Instance(HttpContextBase httpContext)
        {
            Ensure.That(httpContext, "httpContext").IsNotNull();

            ErrorController controller = new ErrorController();
            RouteData data;
            MvcHandler handler = httpContext.Handler as MvcHandler;
            if (handler == null || handler.RequestContext == null || handler.RequestContext.RouteData == null)
            {
                data = new RouteData();
                data.Values.Add(Constants.RouteDataController, Constants.RouteDataControllerNotFound);
                data.Values.Add(Constants.RouteDataAction, Constants.RouteDataActionNotFound);
            }
            else
            {
                data = handler.RequestContext.RouteData;
            }
            controller.ControllerContext = new ControllerContext(httpContext, data, controller);
            return controller;
        }
    }
}
