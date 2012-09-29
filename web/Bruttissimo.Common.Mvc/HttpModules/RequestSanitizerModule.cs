using System;
using System.Web;
using Bruttissimo.Common.InversionOfControl;
using Bruttissimo.Common.Mvc.HttpModules.Wiring;
using Bruttissimo.Common.Mvc.Utility;

namespace Bruttissimo.Common.Mvc.HttpModules
{
    [ApplicationModule]
    public class RequestSanitizerModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
        }

        public void Dispose()
        {
        }

        protected void BeginRequest(object sender, EventArgs args)
        {
            RequestSanitizer sanitizer = IoC.Container.Resolve<RequestSanitizer>();
            sanitizer.ValidateUrl();
        }
    }
}
