using System.Web;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Extensibility;

namespace Bruttissimo.Domain.Logic.MiniMembership
{
    public class AclModule : IAclModule
    {
        public bool IsAccessibleToUser(IControllerTypeResolver controllerTypeResolver, DefaultSiteMapProvider provider, HttpContext context, SiteMapNode node)
        {
            return true;
        }
    }
}