using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity.Entities;

namespace Bruttissimo.Domain.Repository
{
    public interface ILogRepository
    {
        IEnumerable<Log> GetLast(int count);

        DateTime? GetFacebookImportDate(string feed);
        FacebookImportLog UpdateFacebookImportLog(FacebookImportLog importLog);
        FacebookExportLog UpdateFacebookExportLog(FacebookExportLog exportLog);
        TwitterExportLog UpdateTwitterExportLog(TwitterExportLog exportLog);
    }
}
