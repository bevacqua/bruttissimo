using System;
using Castle.MicroKernel;
using Quartz;
using Quartz.Spi;

namespace Bruttissimo.Common.Mvc
{
    public class WindsorJobFactory : IJobFactory
    {
        private readonly IKernel kernel;

        public WindsorJobFactory(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            this.kernel = kernel;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            IJobDetail detail = bundle.JobDetail;
            Type jobType = detail.JobType;
            return (IJob)kernel.Resolve(jobType);
        }
    }
}
