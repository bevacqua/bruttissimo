using System;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;

namespace Bruttissimo.Common.Mvc
{
    internal sealed class WindsorActionInvoker : ControllerActionInvoker
    {
        private readonly ActionInvokerFilters filters;

        public WindsorActionInvoker(ActionInvokerFilters filters)
        {
            Ensure.That(filters, "filters").IsNotNull();

            this.filters = filters;
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            FilterInfo filterInfo = base.GetFilters(controllerContext, actionDescriptor);
            foreach (IActionFilter filter in filters.Action)
            {
                filterInfo.ActionFilters.Add(filter);
            }
            foreach (IAuthorizationFilter filter in filters.Authorization)
            {
                filterInfo.AuthorizationFilters.Add(filter);
            }
            foreach (IExceptionFilter filter in filters.Exception)
            {
                filterInfo.ExceptionFilters.Add(filter);
            }
            foreach (IResultFilter filter in filters.Result)
            {
                filterInfo.ResultFilters.Add(filter);
            }
            return filterInfo;
        }
    }
}
