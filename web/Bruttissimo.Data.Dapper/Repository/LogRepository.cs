using System;
using System.Collections.Generic;
using System.Data;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Dapper;

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
			string sql = @"
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
	        throw new NotImplementedException();
	    }

	    public void UpdateFacebookImportDate(string feed, DateTime date)
	    {
	        throw new NotImplementedException();
	    }
	}
}