using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bruttissimo.Common.Extensions;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Helpers;
using Bruttissimo.Common.Resources;
using Quartz;
using log4net;

namespace Bruttissimo.Common.Quartz
{
    public class JobAutoRunner : IJobAutoRunner
    {
        private readonly ILog log = LogManager.GetLogger(typeof(JobAutoRunner));
        private readonly IScheduler scheduler;
        private readonly IList<Type> jobTypes;

        public JobAutoRunner(IScheduler scheduler, IList<Type> jobTypes)
        {
            Ensure.That(scheduler, "scheduler").IsNotNull();
            Ensure.That(jobTypes, "jobTypes").IsNotNull();

            this.scheduler = scheduler;
            this.jobTypes = jobTypes;
        }

        public void Fire()
        {
            scheduler.Start();

            Parallel.ForEach(jobTypes, jobType =>
            {
                AutoRunAttribute configuration = jobType.GetAttribute<AutoRunAttribute>();

                if (configuration == null)
                {
                    return; // sanity.
                }
                log.Debug(Debug.SchedulingAutoRunJob.FormatWith(jobType.Name));

                if (configuration.RunOnce)
                {
                    scheduler.StartJob(jobType);
                }
                else
                {
                    DateTimeOffset now = DateTimeOffset.UtcNow;
                    DateTimeOffset offset = now.AddMinutes(configuration.Delay);

                    int minutes = configuration.Interval ?? AutoRunAttribute.DefaultInterval;

                    IScheduleBuilder schedule = DailyTimeIntervalScheduleBuilder.Create().WithIntervalInMinutes(minutes);
                    ITrigger trigger = TriggerBuilder.Create().StartAt(offset).WithSchedule(schedule).Build();

                    scheduler.ScheduleJob(jobType, trigger);
                }
            });
        }
    }
}
