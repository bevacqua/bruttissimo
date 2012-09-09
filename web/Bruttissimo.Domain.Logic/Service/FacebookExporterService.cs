using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Export(FacebookExportLog entry)
        {
            IList<Post> posts = postRepository.GetPostsPendingFacebookExport().ToList();

            entry.ExportCount = 0;
            entry.PostCount = posts.Count();

            foreach (Post post in posts)
            {
                FacebookPost result = facebookRepository.PostToFeed(post);

                if (result == null) // post failed.
                {
                    continue;
                }
                post.FacebookPostId = result.Id;
                post.FacebookUserId = result.From.Id;
                post.FacebookFeedId = result.To.Data[0].Id;

                postRepository.Update(post);
                entry.ExportCount++;
            }
        }
    }
}