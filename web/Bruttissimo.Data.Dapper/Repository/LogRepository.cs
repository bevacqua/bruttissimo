using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Dapper;

namespace Bruttissimo.Data.Dapper.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly IDbConnection connection;

        public LogRepository(IDbConnection connection)
        {
            Ensure.That(() => connection).IsNotNull();

            this.connection = connection;
        }

        public IEnumerable<Log> GetLast(int count)
        {
            const string sql = @"
				SET ROWCOUNT @count

				SELECT [Log].*
				FROM [Log]
				ORDER BY [Log].[Date] DESC

				SET ROWCOUNT 0
			";
            IEnumerable<Log> logs = connection.Query<Log>(sql, new { count });
            return logs;
        }

        public DateTime? GetFacebookImportDate(string feed)
        {
            const string sql = @"
				SELECT TOP 1 [L].[PostUpdated]
				FROM [FacebookImportLog] [L]
                WHERE [L].[FacebookFeedId] = @feed
				ORDER BY [L].[PostUpdated] DESC
			";
            IEnumerable<FacebookImportLog> logs = connection.Query<FacebookImportLog>(sql, new { feed });
            FacebookImportLog log = logs.FirstOrDefault();
            return log == null ? null : log.PostUpdated;
        }

        public FacebookImportLog UpdateFacebookImportLog(FacebookImportLog importLog)
        {
            connection.Insert(importLog);
            return importLog;
        }

        public FacebookExportLog UpdateFacebookExportLog(FacebookExportLog exportLog)
        {
            connection.Insert(exportLog);
            return exportLog;
        }

        public TwitterExportLog UpdateTwitterExportLog(TwitterExportLog exportLog)
        {
            connection.Insert(exportLog);
            return exportLog;
        }
    }
}
