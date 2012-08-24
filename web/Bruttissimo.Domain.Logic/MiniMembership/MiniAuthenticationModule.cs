using System;
using System.Web;
using Bruttissimo.Common;

namespace Bruttissimo.Domain.Logic
{
    public class MiniAuthenticationModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += PostAuthenticateRequest;
        }

        public void Dispose()
        {
        }

        protected void PostAuthenticateRequest(object sender, EventArgs args)
        {
            IMiniAuthentication miniAuthentication = IoC.Container.Resolve<IMiniAuthentication>();
            miniAuthentication.SetContextPrincipal();
        }
    }
}
