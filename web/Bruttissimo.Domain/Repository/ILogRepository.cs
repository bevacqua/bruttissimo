using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
	public interface ILogRepository
	{
        IEnumerable<Log> GetLast(int count);

        DateTime? GetFacebookImportDate(string feed);
        void UpdateFacebookImportDate(string feed, DateTime date);
	}
}