using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public interface IJobService
    {
        IList<JobDto> GetScheduledJobs();
    }
}