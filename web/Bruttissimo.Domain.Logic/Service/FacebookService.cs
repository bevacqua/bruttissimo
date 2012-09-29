using System;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.DTO.Facebook;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.Service
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookImporterService importerService;
        private readonly IFacebookExporterService exporterService;
        private readonly ILogRepository logRepository;

        public FacebookService(IFacebookImporterService importerService, IFacebookExporterService exporterService, ILogRepository logRepository)
        {
            Ensure.That(importerService, "importerService").IsNotNull();
            Ensure.That(exporterService, "exporterService").IsNotNull();
            Ensure.That(logRepository, "logRepository").IsNotNull();

            this.importerService = importerService;
            this.exporterService = exporterService;
            this.logRepository = logRepository;
        }

        public void Import(string feed)
        {
            Ensure.That(feed, "feed").IsNotNull();

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