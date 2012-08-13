using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;
using Quartz;

namespace Bruttissimo.Domain.Logic
{
    public class JobService : IJobService
    {
        private readonly IScheduler scheduler;
        private readonly IJobTypeStore store;
        private readonly IMapper mapper;

        public JobService(IScheduler scheduler, IJobTypeStore store, IMapper mapper)
        {
            if (scheduler == null)
            {
                throw new ArgumentNullException("scheduler");
            }
            if (store == null)
            {
                throw new ArgumentNullException("store");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.store = store;
            this.scheduler = scheduler;
            this.mapper = mapper;
        }

        public IEnumerable<ScheduledJobDto> GetScheduledJobs()
        {
            IList<IJobExecutionContext> jobs = scheduler.GetCurrentlyExecutingJobs();
            return new List<ScheduledJobDto>();
        }

        public IEnumerable<JobDto> GetAvailableJobs()
        {
            IEnumerable<Type> types = store.All;
            IEnumerable<JobDto> dto = mapper.Map<IEnumerable<Type>, IEnumerable<JobDto>>(types);
            return dto;
        }

        public bool ScheduleJob(Guid guid)
        {
            return false;
        }
    }
}