using System;
using System.Collections.Generic;
using System.Threading;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookRepository facebookRepository;
        private readonly IImporterRepository importerRepository;

        public FacebookService(IFacebookRepository facebookRepository, IImporterRepository importerRepository)
        {
            if (facebookRepository == null)
            {
                throw new ArgumentNullException("facebookRepository");
            }
            this.facebookRepository = facebookRepository;
        }

        public void Import()
        {
            Thread.Sleep(300000);
            DateTime? since = importerRepository.GetLastImportDate(ImportSource.Facebook);
            IEnumerable<FacebookPost> feed = facebookRepository.GetPostsInGroupFeed(null, since);

            // TODO: filter already-saved or posts with existing links.
            // TODO: save posts to DB.
            importerRepository.UpdateLastImportDate(ImportSource.Facebook);
        }
    }

    public interface IImporterRepository
    {
        DateTime? GetLastImportDate(ImportSource source);
        void UpdateLastImportDate(ImportSource source);
    }

    public enum ImportSource
    {
        Facebook
    }
}