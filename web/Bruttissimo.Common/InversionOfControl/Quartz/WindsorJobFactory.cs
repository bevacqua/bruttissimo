using System;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Castle.MicroKernel;
using Quartz;
using Quartz.Spi;
using log4net;

namespace Bruttissimo.Common.InversionOfControl.Quartz
{
    public class WindsorJobFactory : IJobFactory
    {
        private const string EXCEPTION_INSTANTIATING_JOB = "An error occurred instantiating job type {0}";

        private readonly ILog log = LogManager.GetLogger(typeof(WindsorJobFactory));
        private readonly IKernel kernel;

        public WindsorJobFactory(IKernel kernel)
        {
            Ensure.That(() => kernel).IsNotNull();
            this.kernel = kernel;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            IJobDetail detail = bundle.JobDetail;
            Type jobType = detail.JobType;
            try
            {
                IJob job = (IJob)kernel.Resolve(jobType);
                return job;
            }
            catch (Exception exception) // log the issue, then re-throw.
            {
                string message = EXCEPTION_INSTANTIATING_JOB.FormatWith(jobType.Name);
                log.Error(message, exception);
                throw;
            }
        }
    }
}
