using System;
using Bruttissimo.Common.Guard;
using DbUp.Engine.Output;
using log4net;

namespace Bruttissimo.Data.Deployment
{
    public class Log4NetAndConsoleUpgradeLog : IUpgradeLog
    {
        private readonly ILog log;
        private readonly ConsoleUpgradeLog console;

        public Log4NetAndConsoleUpgradeLog(ILog log)
        {
            Ensure.That(log, "log").IsNotNull();

            this.log = log;

            console = new ConsoleUpgradeLog();
        }

        public void WriteInformation(string format, params object[] args)
        {
            log.InfoFormat(format,args);
            console.WriteInformation(format, args);
        }

        public void WriteError(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
            console.WriteError(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            log.WarnFormat(format, args);
            console.WriteWarning(format, args);
        }
    }
}