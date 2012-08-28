using System;
using System.Collections.Generic;
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

            facebookImporterService.Import(posts, importLog);

            importLog.Duration = DateTime.UtcNow - importLog.StartDate;

            logRepository.UpdateFacebookImportLog(importLog);
        }

        public void Export()
        {
            /*
             * TODO: get all posts without a fb post Id
             * TODO: for each post, check if the user has a fb access token that's usable, otherwise use the generic token
             * TODO: check if a target feed Id is specified, otherwise target the default feed Id.
             * TODO: post to the facebook feed.
             * TODO: update the post with the fb post Id, and fb user Id (what about case when post is published with default token?)
             */
            throw new NotImplementedException();
        }
    }
}
