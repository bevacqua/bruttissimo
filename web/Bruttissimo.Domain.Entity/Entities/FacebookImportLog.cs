using System;

namespace Bruttissimo.Domain.Entity.Entities
{
    public class FacebookImportLog
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string FacebookFeedId { get; set; }
        public DateTime? PostUpdated { get; set; }
        public int QueryCount { get; set; }
        public int PostCount { get; set; }
        public int InsertCount { get; set; }
    }
}
