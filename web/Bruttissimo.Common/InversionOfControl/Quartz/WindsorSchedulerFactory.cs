using Bruttissimo.Common.Guard;
using Castle.MicroKernel;
using Quartz;
using Quartz.Core;
using Quartz.Impl;
using Quartz.Spi;

namespace Bruttissimo.Common.InversionOfControl.Quartz
{
    public class WindsorSchedulerFactory : StdSchedulerFactory
    {
        private readonly IKernel kernel;

        public WindsorSchedulerFactory(IKernel kernel)
        {
            Ensure.That(() => kernel).IsNotNull();
            this.kernel = kernel;
        }

        protected override IScheduler Instantiate(QuartzSchedulerResources resources, QuartzScheduler qs)
        {
            IScheduler scheduler = base.Instantiate(resources, qs);
            scheduler.JobFactory = kernel.Resolve<IJobFactory>();
            return scheduler;
        }
    }
}
