using System.Web.Mvc;
using System.Web.Routing;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc.Core.Routing;

namespace Bruttissimo.Common.Mvc.Extensions
{
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Maps the specified URI route, forces lowercase.
        /// </summary>
        public static void MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapRouteLowercase(name, url, defaults, null);
        }

        /// <summary>
        /// Maps the specified URI route, forces lowercase.
        /// </summary>
        public static void MapRouteLowercase(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            Ensure.That(() => routes).IsNotNull();
            Ensure.That(() => url).IsNotNull();

            LowercaseRoute route = new LowercaseRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            if (string.IsNullOrEmpty(name))
            {
                routes.Add(route);
            }
            else
            {
                routes.Add(name, route);
            }
        }
    }
}
