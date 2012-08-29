using System.Web;

namespace Bruttissimo.Common.Mvc
{
    public static class HttpContextExtensions
    {
        public static HttpContextBase Wrap(this HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return null;
            }
            return new HttpContextWrapper(httpContext);
        }
    }
}