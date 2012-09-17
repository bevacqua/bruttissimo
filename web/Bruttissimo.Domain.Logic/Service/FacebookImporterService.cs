using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;
using log4net;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookImporterService : BaseService, IFacebookImporterService
    {
        private readonly IFacebookRepository facebookRepository;
        private readonly IPostRepository postRepository;
        private readonly ILinkRepository linkRepository;
        private readonly IUserRepository userRepository;

        private readonly ILog log = LogManager.GetLogger(typeof(FacebookImporterService));

        public FacebookImporterService(IFacebookRepository facebookRepository, IPostRepository postRepository, ILinkRepository linkRepository, IUserRepository userRepository)
        {
            if (facebookRepository == null)
            {
                throw new ArgumentNullException("facebookRepository");
            }
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }
            if (linkRepository == null)
            {
                throw new ArgumentNullException("linkRepository");
            }
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.facebookRepository = facebookRepository;
            this.postRepository = postRepository;
            this.linkRepository = linkRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Imports a list of posts from Facebook, filters them and inserts into the persistance storage.
        /// </summary>
        public void Import(FacebookImportOptions opts)
        {
            int insertCount = 0;
            const string LINK_EXISTS = "Link exists: {0}";
            const string LINK_INSERTION = "Inserted Link #{0}";
            const string POST_INSERTION = "Inserted Post #{0}";

            IEnumerable<FacebookPost> posts = facebookRepository.GetPostsInFeed(opts);

            foreach (FacebookPost facebookPost in posts)
            {
                if (facebookPost.Link == null || facebookPost.Type != FacebookPostType.Link) // only links.
                {
                    continue;
                }
                Uri uri = new Uri(facebookPost.Link);
                Link link = linkRepository.GetByReferenceUri(uri);
                if (link != null && link.PostId.HasValue) // no need to look up by FacebookPost.Id in the case of imports, looking up by Link.ReferenceUri is enough.
                {
                    log.Debug(LINK_EXISTS.FormatWith(link.ReferenceUri));
                }
                link = mapper.Map<FacebookPost, Link>(facebookPost);
                Post post = mapper.Map<FacebookPost, Post>(facebookPost);
                User user = userRepository.GetByFacebookGraphId(post.FacebookUserId);

                linkRepository.Insert(link);
                log.Debug(LINK_INSERTION.FormatWith(link.Id));

                post.LinkId = link.Id;
                post.UserId = user == null ? (long?)null : user.Id;
                postRepository.Insert(post);
                log.Debug(POST_INSERTION.FormatWith(post.Id));

                insertCount++;
            }

            opts.Log.InsertCount = insertCount;
        }
    }
}
