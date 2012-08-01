using System;
using System.Security.Authentication;
using System.Web.Mvc;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Logic;

namespace Bruttissimo.Mvc.Models
{
	[ModelType(typeof(MiniPrincipal))]
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
			MiniPrincipal principal = controllerContext.HttpContext.User as MiniPrincipal;
			if (principal == null)
			{
				throw new AuthenticationException(Common.Resources.Authentication.UnauthorizedRequest);
			}
			return principal;
		}
	}
}