using System;
using System.Web;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Extensions;

namespace Bruttissimo.Common.Mvc.Core.Models
{
    public class ErrorViewModel : HandleErrorInfo
    {
        private readonly HttpContextBase context;

        public bool NotFound
        {
            get { return Exception.IsHttpNotFound(); }
        }

        public bool DisplayException
        {
            get { return context.Request.CanDisplayDebuggingDetails(); }
        }

        public string Message { get; private set; }

        public ErrorViewModel(HttpContextBase context, Exception exception, string controllerName, string actionName, string message = null)
            : base(exception, controllerName, actionName)
        {
            Ensure.That(context, "context").IsNotNull();

            this.context = context;
            Message = message;
        }
    }
}
