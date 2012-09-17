using System.Web.Routing;

namespace Bruttissimo.Common.Mvc
{
	public interface IUrlHelper
	{
		RequestContext RequestContext { get; }
		RouteCollection RouteCollection { get; }
		string Action(string actionName);
		string Action(string actionName, object routeValues);
		string Action(string actionName, RouteValueDictionary routeValues);
		string Action(string actionName, string controllerName);
		string Action(string actionName, string controllerName, object routeValues);
		string Action(string actionName, string controllerName, RouteValueDictionary routeValues);
		string Action(string actionName, string controllerName, object routeValues, string protocol);
		string Content(string contentPath);
		string Encode(string url);
		bool IsLocalUrl(string url);
		string RouteUrl(object routeValues);
		string RouteUrl(RouteValueDictionary routeValues);
		string RouteUrl(string routeName);
		string RouteUrl(string routeName, object routeValues);
		string RouteUrl(string routeName, RouteValueDictionary routeValues);
		string RouteUrl(string routeName, object routeValues, string protocol);
		string RouteUrl(string routeName, RouteValueDictionary routeValues, string protocol, string hostName);
	}
}