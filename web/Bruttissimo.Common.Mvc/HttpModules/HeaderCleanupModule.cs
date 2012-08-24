using System;
using System.Web;
using System.Web.Mvc;
using Bruttissimo.Common.Resources;

namespace Bruttissimo.Common.Mvc
{
    public class HeaderCleanupModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            MvcHandler.DisableMvcResponseHeader = true;

            context.PreSendRequestHeaders += PreSendRequestHeaders;
        }

        public void Dispose()
        {
        }

        protected void PreSendRequestHeaders(object sender, EventArgs args)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpResponse response = application.Response;
            response.Headers.Remove(Constants.ServerResponseHeader);
        }
    }
}
