using System.IO;
using System.Security.Principal;
using System.Web;
using Bruttissimo.Domain.Logic;
using log4net.Core;
using log4net.Layout.Pattern;

namespace log4net.Layout
{
    public class RequestIdentityPattern : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
            {
                return;
            }
            try
            {
                if (context.Request.IsAuthenticated) // note this won't be true until after HttpApplication.PostAuthenticateRequest for any given request.
                {
                    long? id = GetUserId(context.User);
                    if (id.HasValue)
                    {
                        writer.Write(id.Value);
                    }
                }
            }
            catch (HttpException) // when attempting to access the request in an HttpContext that isn't part of a Request.
            {
                // do nothing.
            }
        }

        protected long? GetUserId(IPrincipal principal)
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
    }
}