using log4net.Core;

namespace Bruttissimo.Domain
{
    public interface ILogRealtimeService
    {
        void Update(LoggingEvent loggingEvent);
    }
}