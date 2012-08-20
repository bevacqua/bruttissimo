using System;
using System.Collections.Generic;
using System.IO;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;
using Newtonsoft.Json;
using log4net;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookImporterService : IFacebookImporterService
    {
        private readonly IPostRepository postRepository;
        private readonly ILinkRepository linkRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        private readonly ILog log = LogManager.GetLogger(typeof(FacebookImporterService));

        public FacebookImporterService(IPostRepository postRepository, ILinkRepository linkRepository, IUserRepository userRepository, IMapper mapper)
        {
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
            this.postRepository = postRepository;
            this.linkRepository = linkRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }


        /// <summary>
        /// Takes a list of posts imported from Facebook, filters them and inserts into the persistance storage.
        /// </summary>
        public void Import(IEnumerable<FacebookPost> posts)
        {
            const string LINK_EXISTS = "Link exists: {0}";
            const string LINK_INSERTION = "Inserted Link #{0}";
            const string POST_INSERTION = "Inserted Post #{0}";

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
            }
        }
    }
}
