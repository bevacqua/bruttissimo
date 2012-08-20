using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Bruttissimo.Data.Dapper
{
    public class LogRepository : ILogRepository
    {
        private readonly IDbConnection connection;

        public LogRepository(IDbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
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
            IEnumerable<Log> logs = connection.Query<Log>(sql, new {count});
            return logs;
        }

        public DateTime? GetFacebookImportDate(string feed)
        {
            const string sql = @"
				SELECT TOP 1 [FacebookImportLog].*
				FROM [FacebookImportLog]
				ORDER BY [FacebookImportLog].[Date] DESC
			";
            IEnumerable<FacebookImportLog> logs = connection.Query<FacebookImportLog>(sql);
            FacebookImportLog log = logs.FirstOrDefault();
            return log == null ? (DateTime?)null : log.Date;
        }

        public FacebookImportLog UpdateFacebookImportDate(string feed, DateTime date, int queryCount)
        {
            FacebookImportLog entity = new FacebookImportLog
            {
                FacebookFeedId = feed,
                Date = date,
                QueryCount = queryCount
            };
            connection.Insert(entity);
            return entity;
        }
    }
}
