using System;
using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookRepository facebookRepository;
        private readonly IImportLogRepository importLogRepository;
        private readonly IPostRepository postRepository;
        private readonly ILinkRepository linkRepository;

        public FacebookService(IFacebookRepository facebookRepository, IImportLogRepository importLogRepository,
                               IPostRepository postRepository, ILinkRepository linkRepository)
        {
            if (facebookRepository == null)
            {
                throw new ArgumentNullException("facebookRepository");
            }
            if (importLogRepository == null)
            {
                throw new ArgumentNullException("importLogRepository");
            }
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }
            if (linkRepository == null)
            {
                throw new ArgumentNullException("linkRepository");
            }
            this.facebookRepository = facebookRepository;
            this.importLogRepository = importLogRepository;
            this.postRepository = postRepository;
            this.linkRepository = linkRepository;
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
                Post existing = postRepository.GetByFacebookPostId(post.Id);
                if (existing != null)
                {
                    break;
                }
                Uri uri = new Uri(post.Link);
                Link link = linkRepository.GetByReferenceUri(uri);
                if (link != null)
                {
                    break;
                }
                // TODO: save post to DB.
            }
            importLogRepository.UpdateLastImportDate(group, last);
        }
    }
}