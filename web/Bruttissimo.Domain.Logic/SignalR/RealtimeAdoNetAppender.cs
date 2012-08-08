using System;
using System.Globalization;
using Bruttissimo.Domain.Logic.Hubs;
using SignalR;
using SignalR.Hubs;
using log4net.Appender;
using log4net.Core;

namespace Bruttissimo.Domain.Logic
{
	public class RealtimeAdoNetAppender : AdoNetAppender
	{
		protected override void Append(LoggingEvent loggingEvent)
		{
			base.Append(loggingEvent);
			SignalClients(loggingEvent);
		}

		private void SignalClients(LoggingEvent loggingEvent)
		{
			LoggingEventData data = loggingEvent.GetLoggingEventData();
			Exception exception = loggingEvent.ExceptionObject;

			IHubContext logHub = GlobalHost.ConnectionManager.GetHubContext<LogHub>();
			logHub.Clients.update(new
			{
				date = data.TimeStamp.ToString(CultureInfo.InvariantCulture),
				thread = data.ThreadName,
				level = data.Level.DisplayName,
				logger = data.LoggerName,
				message = data.Message,
				exception = exception == null ? null : new
				{
					message = exception.Message,
					stackTrace = exception.StackTrace,
					sql = exception.Data.Contains("SQL") ? exception.Data["SQL"] : null
				}
			});
		}
	}
}
