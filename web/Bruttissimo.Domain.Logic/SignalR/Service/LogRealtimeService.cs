using System;
using System.Web;
using Bruttissimo.Common;
using Bruttissimo.Domain.Logic.Hubs;
using log4net.Core;

namespace Bruttissimo.Domain
{
    public class LogRealtimeService : ILogRealtimeService
    {
        private readonly HttpContextBase context;
        private readonly IHubContextWrapper<LogHub> hub;

        public LogRealtimeService(HttpContextBase context, IHubContextWrapper<LogHub> hub)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (hub == null)
            {
                throw new ArgumentNullException("hub");
            }
            this.context = context;
            this.hub = hub;
        }

        public void Update(LoggingEvent loggingEvent)
        {
            if (loggingEvent == null)
            {
                throw new ArgumentNullException("loggingEvent");
            }
            string url = null;
            try
            {
                if (context != null && context.Request != null)
                {
                    url = context.Request.RawUrl;
                }
            }
            catch (HttpException) // when attempting to access the request in an HttpContext that isn't part of a Request.
            {
                // do nothing.
            }
            LoggingEventData data = loggingEvent.GetLoggingEventData();
            Exception exception = loggingEvent.ExceptionObject;
            var json = new
            {
                date = data.TimeStamp.ToInvariantString(),
                thread = data.ThreadName,
                level = data.Level.DisplayName,
                logger = data.LoggerName,
                message = data.Message,
                exception = exception == null ? null : new
                {
                    message = exception.Message,
                    stackTrace = exception.StackTrace,
                    sql = exception.Data.Contains("SQL") ? exception.Data["SQL"] : null
                },
                requestUrl = url,
            };
            hub.Context.Clients.update(json);
        }
    }
}