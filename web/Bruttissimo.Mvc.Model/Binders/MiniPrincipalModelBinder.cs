using System;
using System.Security.Authentication;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;

namespace Bruttissimo.Mvc.Model
{
    [ModelType(typeof(IMiniPrincipal))]
    public class MiniPrincipalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Ensure.That(controllerContext, "controllerContext").IsNotNull();
            Ensure.That(bindingContext, "bindingContext").IsNotNull();

            IMiniPrincipal principal = controllerContext.HttpContext.User as IMiniPrincipal;
            if (principal == null)
            {
                throw new AuthenticationException(Common.Resources.Authentication.UnauthorizedRequest);
            }
            return principal;
        }
    }
}
