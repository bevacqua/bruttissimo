using System;
using System.Web;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Logic.Hubs;
using log4net.Core;

namespace Bruttissimo.Domain.Logic
{
    public class LogRealtimeService : ILogRealtimeService
    {
        private readonly IHubContextWrapper<LogHub> hub;

        public LogRealtimeService(IHubContextWrapper<LogHub> hub)
        {
            Ensure.That(hub, "hub").IsNotNull();
            
            this.hub = hub;
        }

        public void Update(HttpContextBase context, LoggingEvent loggingEvent)
        {
            Ensure.That(loggingEvent, "loggingEvent").IsNotNull();

            LoggingEventData data = loggingEvent.GetLoggingEventData();
            Exception exception = loggingEvent.ExceptionObject;
            string requestUrl = GetRawUrl(context);
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

        private string GetRawUrl(HttpContextBase context)
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
