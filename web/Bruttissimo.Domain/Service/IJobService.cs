using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IJobService
    {
        IEnumerable<ScheduledJobDto> GetScheduledJobs();
        IEnumerable<JobDto> GetAvailableJobs();
        bool ScheduleJob(Guid guid);
    }
}
