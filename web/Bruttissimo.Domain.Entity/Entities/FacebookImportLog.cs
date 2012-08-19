using System;

namespace Bruttissimo.Domain.Entity
{
    public class FacebookImportLog
    {
        public long Id { get; set; }
        public string FacebookFeedId { get; set; }
        public DateTime Date { get; set; }
    }
}
