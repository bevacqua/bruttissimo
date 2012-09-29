using System;

namespace Bruttissimo.Domain.Entity.Entities
{
    public class FacebookExportLog : IExportLog
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public int PostCount { get; set; }
        public int ExportCount { get; set; }
    }
}