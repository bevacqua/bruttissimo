using System;
using System.Web.Mvc;

namespace Bruttissimo.Common.Mvc
{
	/// <summary>
	/// AuthorizeAttribute implementation with a slight tweak that allows authenticated
	/// but unauthorized requests to redirect to the home page instead of the login page.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class ExtendedAuthorizeAttribute : AuthorizeAttribute
	{
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			bool authenticated = filterContext.HttpContext.User.Identity.IsAuthenticated;
			if (!authenticated)
			{
				base.HandleUnauthorizedRequest(filterContext);
			}
			else
			{
				filterContext.Result = RedirectToHome();
			}
		}

		private ActionResult RedirectToHome()
		{
			ActionResult result = IoC.GetApplicationContainer().Resolve<RedirectToHomeResult>();
			return result;
		}
	}
}