using System;
using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common.Guard;
using Castle.MicroKernel;

namespace Bruttissimo.Common.Mvc
{
    internal sealed class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            Ensure.That(kernel, "kernel").IsNotNull();

            this.kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                string message = Resources.Error.ControllerNotFound.FormatWith(requestContext.HttpContext.Request.Path);
                throw new HttpNotFoundException(message);
            }
            return (IController)kernel.Resolve(controllerType); // this also resolves the IActionInvoker.
        }
    }
}
