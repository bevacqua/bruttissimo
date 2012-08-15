using System;
using System.Collections.Generic;
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

        public void Import(string group)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            DateTime? since = importerRepository.GetLastImportDate(group);

            IEnumerable<FacebookPost> feed = facebookRepository.GetPostsInGroupFeed(group, since);

            // TODO: filter already-saved or posts with existing links.
            // TODO: save posts to DB.
            DateTime date = DateTime.FromBinary(12); // TODO: get newest updated_date.
            importerRepository.UpdateLastImportDate(group, date);
        }
    }

    // TODO: add Facebook Graph Id to Importer table objects. how (source!=fb and "group" doesn't even make sense)?
    // TODO: maybe we should rename the entity to FacebookImportLog, por example.
    public interface IImporterRepository
    {
        DateTime? GetLastImportDate(string group);
        void UpdateLastImportDate(string group, DateTime date);
    }
}