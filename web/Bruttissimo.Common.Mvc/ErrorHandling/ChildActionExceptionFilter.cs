using System;
using System.Web.Mvc;
using log4net;

namespace Bruttissimo.Common.Mvc
{
    public class ChildActionExceptionFilter : IExceptionFilter
    {
        private readonly ILog log;
        private readonly ExceptionHelper helper;

        public ChildActionExceptionFilter(ILog log, ExceptionHelper helper)
        {
            if (log == null)
            {
                throw new ArgumentNullException("log");
            }
            if (helper == null)
            {
                throw new ArgumentNullException("helper");
            }
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

        internal protected void OnChildActionException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;

            helper.Log(log, exception, Resources.Error.UnhandledChildActionException);

            ErrorViewModel model = helper.GetErrorViewModel(filterContext.RouteData, exception);
            filterContext.Result = new PartialViewResult
            {
                ViewName = Resources.Constants.ChildActionErrorViewName,
                ViewData = new ViewDataDictionary(model)
            };
            filterContext.ExceptionHandled = true;
        }
    }
}