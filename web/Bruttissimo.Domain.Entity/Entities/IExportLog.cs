using System;

namespace Bruttissimo.Domain.Entity
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