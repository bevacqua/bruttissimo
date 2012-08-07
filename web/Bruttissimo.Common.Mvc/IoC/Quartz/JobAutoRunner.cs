using System;
using System.Collections.Generic;
using Quartz;
using Quartz.Impl;

namespace Bruttissimo.Common.Mvc
{
	public class JobAutoRunner : IJobAutoRunner
	{
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
				JobDetailImpl detail = new JobDetailImpl(jobType.Name, null, jobType);
				ITrigger trigger = TriggerBuilder.Create().ForJob(detail).StartNow().Build();

				scheduler.ScheduleJob(detail, trigger);
			}
			scheduler.Start();
		}
	}
}