using System;
using System.Collections.Generic;
using Quartz;
using log4net;

namespace Bruttissimo.Common
{
    public class JobAutoRunner : IJobAutoRunner
    {
        private readonly ILog log = LogManager.GetLogger(typeof(JobAutoRunner));
        private readonly IScheduler scheduler;
        private readonly IList<Type> jobTypes;

        public JobAutoRunner(IScheduler scheduler, IList<Type> jobTypes)
        {
            if (scheduler == null)
            {
                throw new ArgumentNullException("scheduler");
            }
            if (jobTypes == null)
            {
                throw new ArgumentNullException("jobTypes");
            }
            this.scheduler = scheduler;
            this.jobTypes = jobTypes;
        }

        public void Fire()
        {
            scheduler.Start();

            foreach (Type jobType in jobTypes)
            {
                AutoRunAttribute attribute = jobType.GetAttribute<AutoRunAttribute>();

                if (attribute == null)
                {
                    continue; // sanity.
                }
                log.Debug(Resources.Debug.SchedulingAutoRunJob.FormatWith(jobType.Name));

                if (attribute.RunOnce)
                {
                    scheduler.StartJob(jobType);
                }
                else
                {
                    DateTimeOffset now = DateTimeOffset.UtcNow;
                    DateTimeOffset offset = now.AddMinutes(attribute.Delay);
                    
                    double minutes = attribute.Interval ?? AutoRunAttribute.DefaultInterval;
                    TimeSpan interval = TimeSpan.FromMinutes(minutes);

                    Action<SimpleScheduleBuilder> schedule = s => s.WithInterval(interval);
                    ITrigger trigger = TriggerBuilder.Create().StartAt(offset).WithSimpleSchedule(schedule).Build();

                    scheduler.ScheduleJob(jobType, trigger);
                }
            }
        }
    }
}
