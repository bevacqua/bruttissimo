using System;
using System.Collections.Generic;
using Quartz;
using log4net;

namespace Bruttissimo.Common.Mvc
{
	public class JobAutoRunner : IJobAutoRunner
	{
		private readonly ILog log = LogManager.GetLogger(typeof(JobAutoRunner));
		private readonly IList<Type> jobTypes;

		public JobAutoRunner(IList<Type> jobTypes)
		{
			if (jobTypes == null)
			{
				throw new ArgumentNullException("jobTypes");
			}
			this.jobTypes = jobTypes;
		}

		public void Fire(IScheduler scheduler)
		{
			scheduler.Start();

			foreach (Type jobType in jobTypes)
			{
				log.Debug(Resources.Debug.SchedulingAutoRunJob.FormatWith(jobType.Name));

				IJobDetail detail = JobBuilder.Create(jobType).Build();
				ITrigger trigger = TriggerBuilder.Create().StartNow().Build();
				scheduler.ScheduleJob(detail, trigger);
			}
		}
	}
}