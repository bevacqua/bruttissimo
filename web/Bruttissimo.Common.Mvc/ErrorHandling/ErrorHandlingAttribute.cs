using System;
using System.Web.Mvc;
using log4net;

namespace Bruttissimo.Common.Mvc
{
	public class ErrorHandlingAttribute : HandleErrorAttribute
	{
		private readonly ILog log;
		private readonly ExceptionHelper helper;

		public ErrorHandlingAttribute(Type loggerType, ExceptionHelper helper)
		{
			if (loggerType == null)
			{
				throw new ArgumentNullException("loggerType");
			}
			if (helper == null)
			{
				throw new ArgumentNullException("helper");
			}
			log = LogManager.GetLogger(loggerType);
			this.helper = helper;
		}

		public override void OnException(ExceptionContext filterContext)
		{
			ExceptionFilter filter = new ExceptionFilter(log, helper);
			filter.OnException(filterContext);
		}
	}
}