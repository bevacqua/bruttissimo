using System;
using System.Security.Principal;
using System.Web;

namespace Bruttissimo.Common.Mvc
{
    public static class HttpRequestBaseExtensions
    {
        /// <summary>
        /// Returns a boolean value indicating whether this request can render debugging
        /// information to the response such as exception details or profiling results.
        /// </summary>
        public static bool CanDisplayDebuggingDetails(this HttpRequestBase request)
        {
            bool authorized = false;

            if (request.IsAuthenticated)
            {
                IPrincipal principal = request.RequestContext.HttpContext.User;
                // authorized = principal.IsInRole(Bruttissimo.Domain.Entity.Rights.CanAccessApplicationLogs); // TODO, figure out.
            }

            bool local = false;

            try
            {
                local = request.IsLocal;
            }
            catch (ArgumentException) // for some obscure reason this can throw exceptions.
            {
                // suppress.
            }
            return authorized || local;
        }
    }
}
