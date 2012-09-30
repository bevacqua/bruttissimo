using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Core.Routing;
using Bruttissimo.Common.Mvc.Extensions;
using SignalR;

namespace Bruttissimo.Mvc.Plumbing
{
    internal static class Routing
    {
        private static readonly object notFound = new { controller = "Error", action = "NotFound" };
        
        public static void RegisterAllAreas()
        {
            AreaRegistration.RegisterAllAreas();
        }

        public static void RegisterSignalR(RouteCollection routes)
        {
            Ensure.That(() => routes).IsNotNull();

            // this special route is for SignalR hubs.
            routes.MapHubs("~/realtime");
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            Ensure.That(() => routes).IsNotNull();

            RegisterRouteIgnores(routes);
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
                new { controller = "Error", action = "NotFound", id = UrlParameter.Optional });

            routes.MapRoute("IgnoreHome", "Home", notFound);
            routes.MapRoute("IgnoreIndex", "{controllerName}/Index/{*pathInfo}", notFound);
        }

        internal static void RegisterViewRoutes(RouteCollection routes)
        {
            routes.MapRouteLowercase( // comment upvoting
                "CommentUpVote", "Comments/{id}/Vote/Up",
                new { controller = "Comments", action = "UpVote" },
                new { id = UrlConstraint.RequiredNumeric, httpMethod = new HttpMethodConstraint("POST") });

            routes.MapRouteLowercase( // comment downvoting
                "CommentDownVote", "Comments/{id}/Vote/Down",
                new { controller = "Comments", action = "DownVote" },
                new { id = UrlConstraint.RequiredNumeric, httpMethod = new HttpMethodConstraint("POST") });

            routes.MapRouteLowercase( // commenting route
                "Comment", "Posts/{id}/Comment/{parentId}",
                new { controller = "Comments", action = "Create", parentId = UrlParameter.Optional },
                new { id = UrlConstraint.RequiredNumeric, parentId = UrlConstraint.OptionalNumeric, httpMethod = new HttpMethodConstraint("POST") });

            routes.MapRouteLowercase(
                "Post", "Posts/{id}/{slug}",
                new { controller = "Posts", action = "Details", slug = UrlParameter.Optional }, // slug is optional, although the request will be redirected if the slug is incorrect.
                new { id = UrlConstraint.RequiredNumeric });

            routes.MapRouteLowercase( // this route is used for sharing purposes, it will ultimately result in a permanent redirect.
                "PostShortcut", "P/{id}",
                new { controller = "Posts", action = "Details" },
                new { id = UrlConstraint.RequiredNumeric });

            routes.MapRouteLowercase(
                "Default", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { id = UrlConstraint.OptionalNumeric });
        }
    }
}
