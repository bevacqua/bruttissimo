using System;

namespace Bruttissimo.Domain
{
    public interface IImportLogRepository
    {
        DateTime? GetLastImportDate(string group);
        void UpdateLastImportDate(string group, DateTime date);
    }
}