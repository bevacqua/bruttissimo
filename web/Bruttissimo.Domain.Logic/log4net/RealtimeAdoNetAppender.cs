using System.Web;
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
            HttpContext current = HttpContext.Current;
            HttpContextWrapper context = current == null ? null : new HttpContextWrapper(current);
            ILogRealtimeService realtime = IoC.Container.Resolve<ILogRealtimeService>();
            realtime.Update(context, loggingEvent);
        }
    }
}