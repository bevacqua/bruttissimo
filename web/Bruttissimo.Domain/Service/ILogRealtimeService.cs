using System.Web;
using log4net.Core;

namespace Bruttissimo.Domain.Service
{
    public interface ILogRealtimeService
    {
        void Update(HttpContextBase context, LoggingEvent loggingEvent);
    }
}
