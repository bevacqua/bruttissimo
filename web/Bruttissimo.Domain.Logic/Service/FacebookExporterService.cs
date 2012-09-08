using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookExporterService : IFacebookExporterService
    {
        private readonly IPostRepository postRepository;
        private readonly IFacebookRepository facebookRepository;

        public FacebookExporterService(IPostRepository postRepository, IFacebookRepository facebookRepository)
        {
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }
            if (facebookRepository == null)
            {
                throw new ArgumentNullException("facebookRepository");
            }
            this.postRepository = postRepository;
            this.facebookRepository = facebookRepository;
        }

        public void Export()
        {
            IEnumerable<Post> posts = postRepository.GetPostsPendingFacebookExport();

            foreach (Post post in posts)
            {
                FacebookPost result = facebookRepository.PostToFeed(post);

                post.FacebookPostId = result.Id;
                post.FacebookUserId = result.From.Id;
                post.FacebookFeedId = result.To.Data[0].Id;

                postRepository.Update(post);
            }
        }
    }
}