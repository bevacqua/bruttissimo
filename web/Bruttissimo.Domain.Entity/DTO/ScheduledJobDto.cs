using System;

namespace Bruttissimo.Domain.Entity
{
    public class ScheduledJobDto
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? FireTime { get; set; }
        public TimeSpan RunTime { get; set; }
    }
}