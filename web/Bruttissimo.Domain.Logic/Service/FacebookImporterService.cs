using System;
using System.Collections.Generic;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookImporterService : IFacebookImporterService
    {
        private readonly IPostRepository postRepository;
        private readonly ILinkRepository linkRepository;
        private readonly IUserRepository userRepository;

        private readonly IMapper mapper;

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

        public void Import(IEnumerable<FacebookPost> posts)
        {
            foreach (FacebookPost facebookPost in posts)
            {
                Uri uri = new Uri(facebookPost.Link);
                Link link = linkRepository.GetByReferenceUri(uri);
                if (link != null && link.PostId.HasValue) // no need to look up by FacebookPost Id in the case of imports, looking up by Link Uri is enough.
                {
                    break;
                }
                Post post = mapper.Map<FacebookPost, Post>(facebookPost);
                User user = userRepository.GetByFacebookGraphId(post.FacebookUserId);

                post.UserId = user == null ? (long?)null : user.Id;
                postRepository.Insert(post);
            }
        }
    }
}