using System;
using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Quartz;
using Bruttissimo.Domain.Entity.DTO;
using Bruttissimo.Domain.Service;
using Quartz;

namespace Bruttissimo.Domain.Logic.Service
{
    public class JobService : BaseService, IJobService
    {
        private readonly IScheduler scheduler;
        private readonly IJobTypeStore store;

        public JobService(IScheduler scheduler, IJobTypeStore store)
        {
            Ensure.That(scheduler, "scheduler").IsNotNull();
            Ensure.That(store, "store").IsNotNull();

            this.store = store;
            this.scheduler = scheduler;
        }

        public IEnumerable<ScheduledJobDto> GetScheduledJobs()
        {
            IEnumerable<IJobExecutionContext> jobs = scheduler.GetCurrentlyExecutingJobs();
            IEnumerable<ScheduledJobDto> dto = mapper.Map<IEnumerable<IJobExecutionContext>, IEnumerable<ScheduledJobDto>>(jobs);
            return dto;
        }

        public IEnumerable<JobDto> GetAvailableJobs()
        {
            IEnumerable<Type> types = store.All;
            IEnumerable<JobDto> dto = mapper.Map<IEnumerable<Type>, IEnumerable<JobDto>>(types);
            return dto;
        }

        public bool ScheduleJob(Guid guid)
        {
            IEnumerable<Type> types = store.All;
            Type type = types.FirstOrDefault(t => t.GUID == guid);
            if (type == null) // sanity
            {
                return false;
            }
            scheduler.StartJob(type);
            return true;
        }
    }
}
