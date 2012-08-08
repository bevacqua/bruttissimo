using System;
using System.Collections.Generic;
using Quartz;
using Quartz.Impl;
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
			foreach (Type jobType in jobTypes)
			{
				log.Debug(Resources.Debug.SchedulingAutoRunJob.FormatWith(jobType.Name));

				JobDetailImpl detail = new JobDetailImpl(jobType.Name, null, jobType);
				ITrigger trigger = TriggerBuilder.Create().ForJob(detail).StartNow().Build();

				scheduler.ScheduleJob(detail, trigger);
			}
			scheduler.Start();
		}
	}
}