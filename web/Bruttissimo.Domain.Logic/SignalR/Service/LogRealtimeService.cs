using System;
using System.Web;
using Bruttissimo.Common;
using Bruttissimo.Domain.Logic;
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
            LoggingEventData data = loggingEvent.GetLoggingEventData();
            Exception exception = loggingEvent.ExceptionObject;
            string requestUrl = GetRawUrl();
            long? userId = context.GetUserId();

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
                    sql = exception.Data["SQL"]
                },
                requestUrl,
                userId
            };
            hub.Context.Clients.update(json);
        }

        private string GetRawUrl()
        {
            try
            {
                if (context != null && context.Request != null)
                {
                    return context.Request.RawUrl;
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