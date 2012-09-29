using System;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Core.Models;
using Bruttissimo.Common.Mvc.Utility;
using Bruttissimo.Common.Resources;
using log4net;

namespace Bruttissimo.Common.Mvc.Core.ErrorHandling
{
    public class ChildActionExceptionFilter : IExceptionFilter
    {
        private readonly ILog log;
        private readonly ExceptionHelper helper;

        public ChildActionExceptionFilter(ILog log, ExceptionHelper helper)
        {
            Ensure.That(log, "log").IsNotNull();
            Ensure.That(helper, "helper").IsNotNull();

            this.log = log;
            this.helper = helper;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            if (filterContext.IsChildAction)
            {
                OnChildActionException(filterContext);
            }
        }

        protected internal void OnChildActionException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;

            helper.Log(log, exception, Error.UnhandledChildActionException);

            ErrorViewModel model = helper.GetErrorViewModel(filterContext.RouteData, exception);
            filterContext.Result = new PartialViewResult
            {
                ViewName = Constants.ChildActionErrorViewName,
                ViewData = new ViewDataDictionary(model)
            };
            filterContext.ExceptionHandled = true;
        }
    }
}
