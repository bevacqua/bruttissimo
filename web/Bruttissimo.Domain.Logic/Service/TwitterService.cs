using System;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class TwitterService : ITwitterService
    {
        private readonly ITwitterExporterService exporterService;
        private readonly ILogRepository logRepository;

        public TwitterService(ITwitterExporterService exporterService, ILogRepository logRepository)
        {
            Ensure.That(exporterService, "exporterService").IsNotNull();
            Ensure.That(logRepository, "logRepository").IsNotNull();

            this.exporterService = exporterService;
            this.logRepository = logRepository;
        }

        public void Export()
        {
            TwitterExportLog entry = new TwitterExportLog
            {
                StartDate = DateTime.UtcNow
            };
            exporterService.Export(entry);

            entry.Duration = DateTime.UtcNow - entry.StartDate;

            logRepository.UpdateTwitterExportLog(entry);
        }
    }
}