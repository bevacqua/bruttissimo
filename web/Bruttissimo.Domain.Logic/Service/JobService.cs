using System;
using System.Collections.Generic;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
    public class JobService : IJobService
    {
        private readonly IScheduler scheduler;
        private readonly IJobTypeStore store;

        public JobService(IScheduler scheduler, IJobTypeStore store)
        {
            if (scheduler == null)
            {
                throw new ArgumentNullException("scheduler");
            }
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            this.store = store;
            this.scheduler = scheduler;
        }

        public IEnumerable<JobDto> GetScheduledJobs()
        {
            IList<IJobExecutionContext> jobs = scheduler.GetCurrentlyExecutingJobs();
            return new List<JobDto>();
        }

        public IEnumerable<AvailableJobDto> GetAvailableJobs()
        {
            IEnumerable<Type> types = store.All;
            throw new NotImplementedException();
        }
    }
}