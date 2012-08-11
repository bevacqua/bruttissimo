using Bruttissimo.Common.Mvc;
using log4net.Appender;
using log4net.Core;

namespace Bruttissimo.Domain.Logic
{
    public class RealtimeAdoNetAppender : AdoNetAppender
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            base.Append(loggingEvent);

            EmitSignal(loggingEvent);
        }

        private void EmitSignal(LoggingEvent loggingEvent)
        {
            ILogRealtimeService realtime = IoC.GetApplicationContainer().Resolve<ILogRealtimeService>();
            realtime.Update(loggingEvent);
        }
    }
}
