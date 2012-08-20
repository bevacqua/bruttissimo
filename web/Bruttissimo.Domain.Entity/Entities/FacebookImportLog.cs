using System;

namespace Bruttissimo.Domain.Entity
{
    public class FacebookImportLog
    {
        public long Id { get; set; }
        public DateTime ImportDate { get; set; }
        public string FacebookFeedId { get; set; }
        public DateTime PostUpdated { get; set; }
        public int QueryCount { get; set; }
    }
}
