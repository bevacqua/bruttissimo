using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.Mvc.Exceptions;
using Bruttissimo.Common.Mvc.Extensions;
using Bruttissimo.Domain.Logic.MiniMembership.Resources;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;
using log4net;

namespace Bruttissimo.Domain.Logic.MiniMembership
{
    public class MiniAclModule : IAclModule
    {
        private readonly ILog log = LogManager.GetLogger(typeof(MiniAclModule));

        public bool IsAccessibleToUser(IControllerTypeResolver controllerTypeResolver, DefaultSiteMapProvider provider, HttpContext context, SiteMapNode node)
        {
            Ensure.ThatTypeFor(() => node).IsOfType<MvcSiteMapNode>();

            HttpContextBase contextBase = context.Wrap();
            MvcSiteMapNode mvc = (MvcSiteMapNode)node;
            string controller = mvc.Controller;
            string action = mvc.Action;

            bool accessible = IsActionAccessibleToUser(controllerTypeResolver, contextBase, controller, action);
            return accessible;
        }

        internal bool IsActionAccessibleToUser(IControllerTypeResolver resolver, HttpContextBase context, string controllerName, string actionName)
        {
            ControllerBase controller = GetController(context, controllerName, actionName);
            if (controller == null)
            {
                return false;
            }

            // find all AuthorizeAttributes on the controller class and action method.
            IList<AuthorizeAttribute> authorizeAttributes = GetAuthorizeAttributes(controller, actionName).ToList();
            bool validationResult = ValidateAuthorizeAttributes(context.User, authorizeAttributes);
            return validationResult;
        }

        internal ControllerBase GetController(HttpContextBase context, string controllerName, string actionName)
        {
            Ensure.ThatTypeFor(() => context.Handler).IsOfType<MvcHandler>();

            MvcHandler handler = (MvcHandler)context.Handler;
            RequestContext requestContext = handler.RequestContext;
            IController controller;

            try
            {
                controller = ControllerBuilder.Current.GetControllerFactory().CreateController(requestContext, controllerName);
            }
            catch (HttpNotFoundException)
            {
                // if we can't instance the controller, we just issue a warning log and trim the action from the site map.
                log.Warn(Exceptions.MiniAclModule_ControllerNotFound.FormatWith(controllerName, actionName));
                return null;
            }
            Ensure.ThatTypeFor(() => controller).Subclasses<ControllerBase>();

            ControllerBase controllerBase = (ControllerBase)controller;

            if (controllerBase.ControllerContext == null) // just new it up.
            {
                controllerBase.ControllerContext = new ControllerContext(requestContext, controllerBase);
            }
            return controllerBase;
        }

        /// <summary>
        /// Avoids risking things like AmbiguousMatchException, by accessing the controller and action descriptors.
        /// </summary>
        internal IEnumerable<AuthorizeAttribute> GetAuthorizeAttributes(ControllerBase controller, string actionName)
        {
            ControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(controller.GetType());
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(controller.ControllerContext, actionName);

            if (actionDescriptor == null)
            {
                // if we can't find a matching action descriptor, we just issue a warning log and trim the action from the site map.
                log.Warn(Exceptions.MiniAclModule_ActionDescriptorNotFound.FormatWith(controllerDescriptor.ControllerName, actionName));
                return new AuthorizeAttribute[] { new UnauthorizedAttribute() };
            }
            IEnumerable<AuthorizeAttribute> controllerAttributes = controllerDescriptor.GetAttributes<AuthorizeAttribute>();
            IEnumerable<AuthorizeAttribute> actionAttributes = actionDescriptor.GetAttributes<AuthorizeAttribute>();

            return controllerAttributes.Concat(actionAttributes);
        }

        internal bool ValidateAuthorizeAttributes(IPrincipal principal, IList<AuthorizeAttribute> authorizeAttributes)
        {
            if (authorizeAttributes.Count == 0) // unrestricted access.
            {
                return true;
            }
            if (authorizeAttributes.Any(a => a is UnauthorizedAttribute)) // invalid action.
            {
                return false;
            }

            foreach (AuthorizeAttribute authorizeAttribute in authorizeAttributes)
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

        private class UnauthorizedAttribute : AuthorizeAttribute
        {
        }
    }
}