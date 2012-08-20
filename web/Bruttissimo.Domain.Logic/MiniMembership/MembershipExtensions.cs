using System.Security.Principal;
using System.Web;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public static class MembershipExtensions
    {
        public static long? GetUserId(this IPrincipal principal)
        {
            MiniPrincipal miniPrincipal = principal as MiniPrincipal;
            if (miniPrincipal != null && miniPrincipal.User != null)
            {
                return miniPrincipal.User.Id;
            }

            long id;
            if (long.TryParse(principal.Identity.Name, out id))
            {
                return id;
            }
            return null;
        }

        public static long? GetUserId(this HttpContextBase context)
        {
            if (context == null)
            {
                return null;
            }
            try
            {
                if (context.Request.IsAuthenticated) // note this won't be true until after HttpApplication.PostAuthenticateRequest for any given request.
                {
                    long? id = context.User.GetUserId(); // we do it like this because it might be set without a MiniPrincipal being there.
                    return id;
                }
            }
            catch (HttpException) // when attempting to access the request in an HttpContext that isn't part of a Request.
            {
                // suppress.
            }
            return null;
        }

        public static User GetUser(this HttpContextBase context)
        {
            if (context == null)
            {
                return null;
            }
            try
            {
                if (context.Request.IsAuthenticated) // note this won't be true until after HttpApplication.PostAuthenticateRequest for any given request.
                {
                    MiniPrincipal miniPrincipal = context.User as MiniPrincipal;
                    if (miniPrincipal != null)
                    {
                        return miniPrincipal.User;
                    }
                }
            }
            catch (HttpException) // when attempting to access the request in an HttpContext that isn't part of a Request.
            {
                // suppress.
            }
            return null;
        }
    }
}
