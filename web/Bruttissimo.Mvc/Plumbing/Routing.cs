using System;
using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common.Mvc;
using SignalR;

namespace Bruttissimo.Mvc
{
    internal static class Routing
    {
        private static readonly object notFound = new {controller = "Error", action = "NotFound"};

        public static void RegisterRoutes(RouteCollection routes)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            RegisterRouteIgnores(routes);

            // this special route is for SignalR hubs.
            routes.MapHubs("~/realtime");

            RegisterViewRoutes(routes);

            // this route is intended to catch 404 Not Found errors instead of bubbling them all the way up to IIS.
            routes.MapRoute("PageNotFound", "{*catchall}", notFound);
        }

        internal static void RegisterRouteIgnores(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // by not using .IgnoreRoute I avoid IIS taking over my custom error handling engine.
            routes.MapRoute(
                "IgnorePostDetails", "Posts/Details/{id}",
                new {controller = "Error", action = "NotFound", id = UrlParameter.Optional}
                );

            routes.MapRoute("IgnoreHome", "Home", notFound);
            routes.MapRoute("IgnoreIndex", "{controllerName}/Index/{*pathInfo}", notFound);
        }

        internal static void RegisterViewRoutes(RouteCollection routes)
        {
            routes.MapRouteLowercase(
                "Post", "posts/{id}/{slug}",
                new {controller = "Posts", action = "Details", slug = UrlParameter.Optional}, // slug is optional, although the request will be redirected if the slug is incorrect.
                new {id = UrlConstraint.RequiredNumeric}
                );

            routes.MapRouteLowercase( // this route is used for sharing purposes, it will ultimately result in a permanent redirect.
                "PostShortcut", "p/{id}",
                new {controller = "Posts", action = "Details"},
                new {id = UrlConstraint.RequiredNumeric}
                );

            routes.MapRouteLowercase(
                "Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                new {id = UrlConstraint.OptionalNumeric}
                );
        }
    }
}
