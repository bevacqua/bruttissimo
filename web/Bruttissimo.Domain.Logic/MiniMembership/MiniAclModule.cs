using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.Mvc.Extensions;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;

namespace Bruttissimo.Domain.Logic.MiniMembership
{
    public class MiniAclModule : IAclModule
    {
        public bool IsAccessibleToUser(IControllerTypeResolver controllerTypeResolver, DefaultSiteMapProvider provider, HttpContext context, SiteMapNode node)
        {
            Ensure.ThatTypeFor(node).IsOfType<MvcSiteMapNode>();

            HttpContextBase contextBase = context.Wrap();
            MvcSiteMapNode mvc = (MvcSiteMapNode)node;
            string controller = mvc.Controller;
            string action = mvc.Action;

            bool accessible = IsActionAccessibleToUser(contextBase, controller, action);
            return accessible;
        }

        internal bool IsActionAccessibleToUser(HttpContextBase context, string controllerName, string actionName)
        {
            MvcHandler handler = context.Handler as MvcHandler;

            if (handler == null)
            {
                return false;
            }

            IController controller = ControllerBuilder.Current.GetControllerFactory().CreateController(handler.RequestContext, controllerName);
            Type controllerType = controller.GetType();

            // find all AuthorizeAttributes on the controller class and action method.
            IEnumerable<AuthorizeAttribute> controllerAttributes = controllerType.GetAttributes<AuthorizeAttribute>();
            IEnumerable<AuthorizeAttribute> actionAttributes = controllerType.GetMethod(actionName).GetAttributes<AuthorizeAttribute>();

            IList<AuthorizeAttribute> list = controllerAttributes.Concat(actionAttributes).ToList();

            if (list.Count == 0) // unrestricted access.
            {
                return true;
            }
            IPrincipal principal = context.User;

            foreach (AuthorizeAttribute authorizeAttribute in list)
            {
                string roles = authorizeAttribute.Roles;

                if (!SufficientAccessValidation(principal, roles))
                {
                    return false;
                }
            }
            return true;
        }

        internal bool SufficientAccessValidation(IPrincipal principal, string roles)
        {
            if (roles.NullOrEmpty()) // no roles, then all we need to be is authenticated.
            {
                return principal.Identity.IsAuthenticated;
            }

            string[] roleArray = roles.Split(',');

            if (roleArray.Any(role => role == "*")) // if role contains "*", unrestricted access.
            {
                return true;
            }

            if (roleArray.Any(principal.IsInRole)) // does principal have access to any role in the list?
            {
                return true;
            }
            return false;
        }
    }
}