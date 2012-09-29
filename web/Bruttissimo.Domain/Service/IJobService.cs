using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity.DTO;

namespace Bruttissimo.Domain.Service
{
    public interface IJobService
    {
        IEnumerable<ScheduledJobDto> GetScheduledJobs();
        IEnumerable<JobDto> GetAvailableJobs();
        bool ScheduleJob(Guid guid);
    }
}
