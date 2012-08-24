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
                log.Debug(Resources.Debug.SchedulingAutoRunJob.FormatWith(jobType.Name));
                scheduler.ScheduleJob(jobType);
            }
        }
    }
}
