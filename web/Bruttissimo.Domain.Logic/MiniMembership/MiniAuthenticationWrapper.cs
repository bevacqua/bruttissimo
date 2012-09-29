using System;
using System.Security.Principal;
using System.Web;
using Bruttissimo.Common.Guard;
using Castle.MicroKernel;
using log4net;

namespace Bruttissimo.Domain.Logic
{
    public class MiniAuthenticationWrapper : IMiniAuthentication
    {
        private readonly ILog log = LogManager.GetLogger(typeof (MiniAuthenticationWrapper));
        private readonly IKernel kernel;
        private readonly HttpContextBase context;

        public MiniAuthenticationWrapper(IKernel kernel, HttpContextBase context)
        {
            Ensure.That(kernel, "kernel").IsNotNull();
            Ensure.That(context, "context").IsNotNull();

            this.kernel = kernel;
            this.context = context;
        }

        public void SetContextPrincipal()
        {
            try
            {
                MiniAuthentication miniAuthentication = kernel.Resolve<MiniAuthentication>();
                miniAuthentication.SetContextPrincipal();
            }
            catch (Exception exception) // at worst, users won't be able to authenticate.
            {
                if (context.Request.IsAuthenticated)
                {
                    IIdentity identity = new MiniIdentity(null, context.User.Identity.AuthenticationType);
                    context.User = new GenericPrincipal(identity, null);
                }
                log.Fatal(exception);
            }
        }
    }
}
