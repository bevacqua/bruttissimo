using System;
using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookRepository facebookRepository;
        private readonly ILogRepository logRepository;
        private readonly IFacebookImporterService facebookImporterService;

        public FacebookService(IFacebookRepository facebookRepository, ILogRepository logRepository, IFacebookImporterService facebookImporterService)
        {
            if (facebookRepository == null)
            {
                throw new ArgumentNullException("facebookRepository");
            }
            if (logRepository == null)
            {
                throw new ArgumentNullException("logRepository");
            }
            if (facebookImporterService == null)
            {
                throw new ArgumentNullException("facebookImporterService");
            }
            this.facebookRepository = facebookRepository;
            this.logRepository = logRepository;
            this.facebookImporterService = facebookImporterService;
        }

        public void Import(string feed)
        {
            if (feed == null)
            {
                throw new ArgumentNullException("feed");
            }
            FacebookImportLog importLog = new FacebookImportLog
            {
                FacebookFeedId = feed,
                StartDate = DateTime.UtcNow
            };
            DateTime? since = logRepository.GetFacebookImportDate(feed);
            IEnumerable<FacebookPost> posts = facebookRepository.GetPostsInFeed(feed, since, importLog);

            if (!posts.Any()) // no new posts.
            {
                return;
            }
            
            facebookImporterService.Import(posts, importLog);

            importLog.Duration = DateTime.UtcNow - importLog.StartDate;

            logRepository.UpdateFacebookImportLog(importLog);
        }
    }
}
