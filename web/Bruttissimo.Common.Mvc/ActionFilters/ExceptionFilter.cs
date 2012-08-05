using System;
using System.Web.Mvc;
using log4net;

namespace Bruttissimo.Common.Mvc
{
	public class ExceptionFilter: IExceptionFilter
	{
		private readonly ILog log;
		private readonly ExceptionHelper helper;

		public ExceptionFilter(ILog log, ExceptionHelper helper)
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
			if (filterContext.HttpContext.Request.IsAjaxRequest())
			{
				OnAjaxException(filterContext);
			}
			else if (filterContext.IsChildAction)
			{
				OnChildActionException(filterContext);
			}
			else
			{
				OnRegularException(filterContext);
			}
		}

		internal protected void OnRegularException(ExceptionContext filterContext)
		{
			Exception exception = filterContext.Exception;

			log.Error(Resources.Error.UnhandledException, exception);

			filterContext.HttpContext.Response.Clear();
			filterContext.HttpContext.Response.Status = Resources.Constants.HttpServerError;
			filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

			ErrorViewModel model = helper.GetErrorViewModel(filterContext.RouteData, exception);
			filterContext.Result = new ViewResult
				{
					ViewName = Resources.Constants.ErrorViewName,
					ViewData = new ViewDataDictionary(model)
				};
			filterContext.ExceptionHandled = true;
		}

		internal protected void OnChildActionException(ExceptionContext filterContext)
		{
			Exception exception = filterContext.Exception;

			log.Error(Resources.Error.UnhandledChildActionException, exception);

			ErrorViewModel model = helper.GetErrorViewModel(filterContext.RouteData, exception);
			filterContext.Result = new PartialViewResult
				{
					ViewName = Resources.Constants.ChildActionErrorViewName,
					ViewData = new ViewDataDictionary(model)
				};
			filterContext.ExceptionHandled = true;
		}

		internal protected void OnAjaxException(ExceptionContext filterContext)
		{
			Exception exception = filterContext.Exception;

			log.Error(Resources.Error.UnhandledAjaxException, exception);

			filterContext.HttpContext.Response.Clear();
			filterContext.HttpContext.Response.Status = Resources.Constants.HttpSuccess;

			string errorMessage = helper.GetMessage(exception, true);

			filterContext.Result = new ExceptionJsonResult(new[] { errorMessage });
			filterContext.ExceptionHandled = true;
		}
	}
}