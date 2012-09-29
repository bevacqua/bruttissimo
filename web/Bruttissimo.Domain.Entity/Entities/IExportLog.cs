using System;

namespace Bruttissimo.Domain.Entity.Entities
{
    public interface IExportLog
    {
        long Id { get; set; }
        DateTime StartDate { get; set; }
        TimeSpan Duration { get; set; }
        int PostCount { get; set; }
        int ExportCount { get; set; }
    }
}