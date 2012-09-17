using System.Web.Mvc;
using System.Web.Routing;

namespace Bruttissimo.Common.Mvc
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
	}
}