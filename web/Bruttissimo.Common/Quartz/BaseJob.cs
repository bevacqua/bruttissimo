using System;
using System.Diagnostics;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Resources;
using Quartz;
using log4net;
using Debug = Bruttissimo.Common.Resources.Debug;

namespace Bruttissimo.Common.Quartz
{
    public abstract class BaseJob : IJob, IDisposable
    {
        private readonly Type concreteType;
        private readonly ILog log;

        protected BaseJob()
        {
            concreteType = GetType();
            log = LogManager.GetLogger(concreteType);
        }

        public abstract void DoWork(IJobExecutionContext context);

        public void Execute(IJobExecutionContext context)
        {
            string id = context.FireInstanceId;
            string name = concreteType.FullName;
            log.Info(Debug.JobExecuting.FormatWith(name, id));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                DoWork(context);
            }
            catch (Exception exception)
            {
                log.Error(Error.UnhandledException, exception); // log here too, because the wrapper clears the stack trace.
                throw new JobExecutionException(exception);
            }
            finally
            {
                stopwatch.Stop();
                string duration = stopwatch.Elapsed.ToShortDurationString();
                log.Info(Debug.JobExecuted.FormatWith(name, id, duration));

                Dispose();
            }
        }

        public virtual void Dispose() // virtual so castle proxies it, and the interceptor is able to catch invocations.
        {
            // just intended to be captured by the container's release interceptor.
        }
    }
}
