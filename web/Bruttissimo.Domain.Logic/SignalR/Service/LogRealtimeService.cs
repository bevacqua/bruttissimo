using System;
using System.Web;
using Bruttissimo.Common;
using Bruttissimo.Domain.Logic.Hubs;
using SignalR;
using SignalR.Hubs;
using log4net.Core;

namespace Bruttissimo.Domain
{
    public class LogRealtimeService : ILogRealtimeService
    {
        private readonly HttpContextBase context;

        public LogRealtimeService(HttpContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.context = context;
        }

        public void Update(LoggingEvent loggingEvent)
        {
            string url = null;
            try
            {
                if (context != null)
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

            IHubContext logHub = GlobalHost.ConnectionManager.GetHubContext<LogHub>();
            logHub.Clients.update(json);
        }
    }
}