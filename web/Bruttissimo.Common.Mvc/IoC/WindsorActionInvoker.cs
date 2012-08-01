using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bruttissimo.Common.Mvc
{
	public class WindsorActionInvoker : ControllerActionInvoker
	{
		private readonly IList<IActionFilter> actionFilters;
		private readonly IList<IAuthorizationFilter> authorizationFilters;
		private readonly IList<IExceptionFilter> exceptionFilters;
		private readonly IList<IResultFilter> resultFilters;

		public WindsorActionInvoker(IList<IActionFilter> actionFilters, IList<IAuthorizationFilter> authorizationFilters, IList<IExceptionFilter> exceptionFilters, IList<IResultFilter> resultFilters)
		{
			if (actionFilters == null)
			{
				throw new ArgumentNullException("actionFilters");
			}
			if (authorizationFilters == null)
			{
				throw new ArgumentNullException("authorizationFilters");
			}
			if (exceptionFilters == null)
			{
				throw new ArgumentNullException("exceptionFilters");
			}
			if (resultFilters == null)
			{
				throw new ArgumentNullException("resultFilters");
			}
			this.actionFilters = actionFilters;
			this.authorizationFilters = authorizationFilters;
			this.exceptionFilters = exceptionFilters;
			this.resultFilters = resultFilters;
		}

		protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			FilterInfo filterInfo = base.GetFilters(controllerContext, actionDescriptor);
			foreach (IActionFilter filter in actionFilters)
			{
				filterInfo.ActionFilters.Add(filter);
			}
			foreach (IAuthorizationFilter filter in authorizationFilters)
			{
				filterInfo.AuthorizationFilters.Add(filter);
			}
			foreach (IExceptionFilter filter in exceptionFilters)
			{
				filterInfo.ExceptionFilters.Add(filter);
			}
			foreach (IResultFilter filter in resultFilters)
			{
				filterInfo.ResultFilters.Add(filter);
			}
			return filterInfo;
		}
	}
}