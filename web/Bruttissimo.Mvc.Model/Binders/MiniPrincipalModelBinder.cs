using System.Security.Authentication;
using System.Web.Mvc;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.InversionOfControl.Mvc;
using Bruttissimo.Common.Resources;
using Bruttissimo.Domain.MiniMembership;

namespace Bruttissimo.Mvc.Model.Binders
{
    [ModelType(typeof(IMiniPrincipal))]
    public class MiniPrincipalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Ensure.That(() => controllerContext).IsNotNull();
            Ensure.That(() => bindingContext).IsNotNull();

            IMiniPrincipal principal = controllerContext.HttpContext.User as IMiniPrincipal;
            if (principal == null)
            {
                throw new AuthenticationException(Authentication.UnauthorizedRequest);
            }
            return principal;
        }
    }
}
