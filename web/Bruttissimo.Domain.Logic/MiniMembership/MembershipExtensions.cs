﻿using System.Security.Principal;
using System.Web;

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
            try
            {
                if (context.Request.IsAuthenticated) // note this won't be true until after HttpApplication.PostAuthenticateRequest for any given request.
                {
                    long? id = context.User.GetUserId();
                    return id;
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