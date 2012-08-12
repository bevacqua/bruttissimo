using System;
using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Domain.Entity;
using Quartz;

namespace Bruttissimo.Domain
{
    public class JobService : IJobService
    {
        private readonly IScheduler scheduler;

        public JobService(IScheduler scheduler)
        {
            if (scheduler == null)
            {
                throw new ArgumentNullException("scheduler");
            }
            this.scheduler = scheduler;
        }

        public IEnumerable<JobDto> GetScheduledJobs()
        {
            IList<IJobExecutionContext> jobs = scheduler.GetCurrentlyExecutingJobs();
            var job = jobs.First();
            return new List<JobDto>();
        }
    }
}