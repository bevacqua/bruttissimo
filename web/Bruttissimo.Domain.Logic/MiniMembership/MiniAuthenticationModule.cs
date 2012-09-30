using System;
using System.Web;
using Bruttissimo.Common.InversionOfControl;
using Bruttissimo.Common.Mvc.HttpModules.Wiring;
using Bruttissimo.Domain.MiniMembership;

namespace Bruttissimo.Domain.Logic.MiniMembership
{
    [ApplicationModule(100)]
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
