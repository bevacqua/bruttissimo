using System;
using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain
{
    public class LogService : ILogService
    {
        private readonly ILogRepository logRepository;

        public LogService(ILogRepository logRepository)
        {
            Ensure.That(logRepository, "logRepository").IsNotNull();

            this.logRepository = logRepository;
        }

        public IList<Log> GetLast(int count)
        {
            IEnumerable<Log> logs = logRepository.GetLast(count);
            return logs.ToList();
        }
    }
}
