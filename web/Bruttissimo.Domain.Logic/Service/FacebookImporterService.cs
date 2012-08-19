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
        private readonly IMapper mapper;

        public FacebookImporterService(IPostRepository postRepository, ILinkRepository linkRepository, IMapper mapper)
        {
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }
            if (linkRepository == null)
            {
                throw new ArgumentNullException("linkRepository");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.postRepository = postRepository;
            this.linkRepository = linkRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Takes a list of posts imported from Facebook, filters them and inserts into the persistance storage.
        /// </summary>
        public void Import(IEnumerable<FacebookPost> posts)
        {
            foreach (FacebookPost facebookPost in posts)
            {
                Uri uri = new Uri(facebookPost.Link);
                Link link = linkRepository.GetByReferenceUri(uri);
                if (link != null) // no need to look up by FacebookPost Id in the case of imports, looking up by Link Uri is enough.
                {
                    break;
                }
                // TODO: automap link from fbpost
                // TODO: automap post from fbpost
                Post post = mapper.Map<FacebookPost, Post>(facebookPost);
                // TODO: link user to post, if one is found with accessToken == user id.
                // NOTE: the post is always linked with the facebook user Id that posted it, though.
                postRepository.Insert(post);
            }
        }
    }
}