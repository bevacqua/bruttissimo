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

        public void Import(IEnumerable<FacebookPost> posts)
        {
            foreach (FacebookPost post in posts)
            {
                Uri uri = new Uri(post.Link);
                Link link = linkRepository.GetByReferenceUri(uri);
                if (link != null) // no need to look up by FacebookPost Id in the case of imports, looking up by Link Uri is enough.
                {
                    break;
                }
                // TODO: automap link from fbpost
                // TODO: automap post from fbpost
                // TODO: link user to post?
                // TODO: save post to DB.
            }
        }
    }
}