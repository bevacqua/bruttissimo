using System;
using Quartz;

namespace Bruttissimo.Common
{
    public static class QuartzExtensions
    {
        /// <summary>
        /// Schedules a job of the provided type to run immediatly.
        /// </summary>
        public static DateTimeOffset ScheduleJob(this IScheduler scheduler, Type jobType)
        {
            IJobDetail detail = JobBuilder.Create(jobType).Build();
            ITrigger trigger = TriggerBuilder.Create().StartNow().Build();
            return scheduler.ScheduleJob(detail, trigger);
        }
    }
}