using System;
using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.Mvc.Interface;

namespace Bruttissimo.Common.Mvc.Utility
{
    public class UrlHelperWrapper : UrlHelper, IUrlHelper
    {
        public UrlHelperWrapper(RequestContext requestContext)
            : base(requestContext)
        {
        }

        public UrlHelperWrapper(RequestContext requestContext, RouteCollection routeCollection)
            : base(requestContext, routeCollection)
        {
        }

        public string PublicRouteUrl(string routeName, object routeValues)
        {
            string route = RouteUrl(routeName, routeValues, "http");
            return MakePublic(route);
        }

        public string PublicAction(string action, string controller, object routeValues)
        {
            string route = Action(action, controller, routeValues, "http");
            return MakePublic(route);
        }

        /// <summary>
        /// Produces an absolute Uri for a given route, which can be used from any external resource.
        /// </summary>
        internal string MakePublic(string route)
        {
            Uri uri = new Uri(route);
            string publicUri = uri.WithPublicPort().AbsoluteUri;
            return publicUri;
        }
    }
}