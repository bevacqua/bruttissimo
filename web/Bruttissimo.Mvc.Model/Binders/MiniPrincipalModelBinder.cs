using System;
using System.Security.Authentication;
using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;

namespace Bruttissimo.Mvc.Model
{
	[ModelType(typeof(IMiniPrincipal))]
	public class MiniPrincipalModelBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (bindingContext == null)
			{
				throw new ArgumentNullException("bindingContext");
			}
			IMiniPrincipal principal = controllerContext.HttpContext.User as IMiniPrincipal;
			if (principal == null)
			{
				throw new AuthenticationException(Common.Resources.Authentication.UnauthorizedRequest);
			}
			return principal;
		}
	}
}