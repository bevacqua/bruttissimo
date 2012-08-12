using System.IO;
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
            AppendUserId(writer, new HttpContextWrapper(context));
        }

        private void AppendUserId(TextWriter writer, HttpContextBase context)
        {
            long? id = context.GetUserId();
            if (id.HasValue)
            {
                writer.Write(id.Value);
            }
        }
    }
}