using System.Collections.Generic;
using Bruttissimo.Mvc.Model;

namespace Bruttissimo.Domain.Logic
{
    public interface IJobService
    {
        IList<JobDto> GetScheduledJobs();
    }
}