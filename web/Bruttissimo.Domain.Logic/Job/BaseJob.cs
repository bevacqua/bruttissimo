using System;
using Castle.MicroKernel;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
    public abstract class BaseJob : IJob
    {
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
        }

        public abstract void DoWork(IJobExecutionContext context);

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                DoWork(context);
            }
            catch (Exception exception)
            {
                throw new JobExecutionException(exception);
            }
            finally
            {
                kernel.ReleaseComponent(dependencies);
            }
        }
    }
}