using System;
using System.Diagnostics;
using Bruttissimo.Common;
using Castle.MicroKernel;
using Quartz;
using log4net;

namespace Bruttissimo.Domain.Logic
{
    public abstract class BaseJob : IJob
    {
        private readonly Type concreteType;
        private readonly ILog log;
        private readonly IKernel kernel;
        private readonly object[] dependencies;

        protected BaseJob(IKernel kernel, params object[] dependencies)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            this.kernel = kernel;
            this.dependencies = dependencies ?? new object[0];

            concreteType = GetType();
            log = LogManager.GetLogger(concreteType);
        }

        public abstract void DoWork(IJobExecutionContext context);

        public void Execute(IJobExecutionContext context)
        {
            log.Info(Common.Resources.Debug.JobExecuting.FormatWith(concreteType.FullName, context.FireInstanceId));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                DoWork(context);
            }
            catch (Exception exception)
            {
                log.Error(Common.Resources.Error.UnhandledException, exception);
                throw new JobExecutionException(exception);
            }
            finally
            {
                stopwatch.Stop();
                log.Info(Common.Resources.Debug.JobExecuted.FormatWith(concreteType.FullName, context.FireInstanceId, stopwatch.Elapsed.ToShortDurationString()));

                foreach (object dependency in dependencies) // PerThread lifestyle dependencies are released when application shuts down.
                {
                    kernel.ReleaseComponent(dependency); // release them when the job completes execution instead.
                }
            }
        }
    }
}