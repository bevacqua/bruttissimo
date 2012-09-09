using System;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class TwitterService : ITwitterService
    {
        private readonly ILogRepository logRepository;

        public TwitterService(ILogRepository logRepository)
        {
            if (logRepository == null)
            {
                throw new ArgumentNullException("logRepository");
            }
            this.logRepository = logRepository;
        }

        public void Export()
        {
            TwitterExportLog entry = new TwitterExportLog
            {
                StartDate = DateTime.UtcNow
            };
            // TODO: actual exporting.

            entry.Duration = DateTime.UtcNow - entry.StartDate;

            logRepository.UpdateTwitterExportLog(entry);
        }
    }
}