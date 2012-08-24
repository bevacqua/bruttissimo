using System;
using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public class LogService : ILogService
    {
        private readonly ILogRepository logRepository;

        public LogService(ILogRepository logRepository)
        {
            if (logRepository == null)
            {
                throw new ArgumentNullException("logRepository");
            }
            this.logRepository = logRepository;
        }

        public IList<Log> GetLast(int count)
        {
            IEnumerable<Log> logs = logRepository.GetLast(count);
            return logs.ToList();
        }
    }
}
