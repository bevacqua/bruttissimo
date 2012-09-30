using System;
using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Exceptions;
using Bruttissimo.Common.Resources;
using Castle.MicroKernel;

namespace Bruttissimo.Common.Mvc.InversionOfControl.Mvc
{
    internal sealed class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            Ensure.That(() => kernel).IsNotNull();

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
                string message = Error.ControllerNotFound.FormatWith(requestContext.HttpContext.Request.Path);
                throw new HttpNotFoundException(message);
            }
            return (IController)kernel.Resolve(controllerType); // this also resolves the IActionInvoker.
        }
    }
}
