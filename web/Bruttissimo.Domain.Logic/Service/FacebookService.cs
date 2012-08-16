using System;
using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookRepository facebookRepository;
        private readonly IImportLogRepository importLogRepository;

        public FacebookService(IFacebookRepository facebookRepository, IImportLogRepository importLogRepository)
        {
            if (facebookRepository == null)
            {
                throw new ArgumentNullException("facebookRepository");
            }
            if (importLogRepository == null)
            {
                throw new ArgumentNullException("importLogRepository");
            }
            this.facebookRepository = facebookRepository;
            this.importLogRepository = importLogRepository;
        }

        public void Import(string group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            DateTime? since = importLogRepository.GetLastImportDate(group);

            IEnumerable<FacebookPost> feed = facebookRepository.GetPostsInGroupFeed(group, since);

            if (!feed.Any()) // no new posts.
            {
                return;
            }
            DateTime last = feed.Max(p => p.UpdatedTime);
            foreach (FacebookPost post in feed)
            {
                // TODO: filter already-saved or posts with existing links.
                // TODO: save valid posts to DB.
            }
            importLogRepository.UpdateLastImportDate(group, last);
        }
    }

    // TODO: add Facebook Graph Id to Importer table objects. how (source!=fb and "group" doesn't even make sense)?
    // TODO: maybe we should rename the entity to FacebookImportLog, por example.
    public interface IImportLogRepository
    {
        DateTime? GetLastImportDate(string group);
        void UpdateLastImportDate(string group, DateTime date);
    }
}