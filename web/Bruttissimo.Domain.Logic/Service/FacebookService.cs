using System;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookImporterService importerService;
        private readonly IFacebookExporterService exporterService;
        private readonly ILogRepository logRepository;

        public FacebookService(ILogRepository logRepository, IFacebookImporterService importerService, IFacebookExporterService exporterService)
        {
            if (importerService == null)
            {
                throw new ArgumentNullException("importerService");
            }
            if (exporterService == null)
            {
                throw new ArgumentNullException("exporterService");
            }
            if (logRepository == null)
            {
                throw new ArgumentNullException("logRepository");
            }
            this.importerService = importerService;
            this.exporterService = exporterService;
            this.logRepository = logRepository;
        }

        public void Import(string feed)
        {
            if (feed == null)
            {
                throw new ArgumentNullException("feed");
            }
            FacebookImportLog entry = new FacebookImportLog
            {
                FacebookFeedId = feed,
                StartDate = DateTime.UtcNow
            };
            DateTime? since = logRepository.GetFacebookImportDate(feed);

            FacebookImportOptions options = new FacebookImportOptions
            {
                Feed = feed,
                Log = entry,
                Since = since
            };
            importerService.Import(options);

            entry.Duration = DateTime.UtcNow - entry.StartDate;

            logRepository.UpdateFacebookImportLog(entry);
        }

        public void Export()
        {
            FacebookExportLog entry = new FacebookExportLog
            {
                StartDate = DateTime.UtcNow
            };
            exporterService.Export(entry);

            entry.Duration = DateTime.UtcNow - entry.StartDate;

            logRepository.UpdateFacebookExportLog(entry);
        }
    }
}