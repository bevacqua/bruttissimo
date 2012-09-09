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

            int exportCount = 0;

            foreach (Post post in posts)
            {
                string userAccessToken = GetUserAccessToken(post);
                FacebookPost result = facebookRepository.PostToFeed(post, userAccessToken);

                if (result == null) // post failed.
                {
                    continue;
                }
                post.FacebookPostId = result.Id;
                post.FacebookUserId = result.From.Id;
                post.FacebookFeedId = result.To.Data[0].Id;

                postRepository.Update(post);
                exportCount++;
            }
            entry.ExportCount = exportCount;
            entry.PostCount = posts.Count;
        }

        internal string GetUserAccessToken(Post post)
        {
            if (post.User == null)
            {
                return null;
            }
            throw new NotImplementedException();
            /*
             * TODO: verify the user has a facebook connection
             * TODO: verify the user allows posts to be posted to facebook on his behalf
             * TODO: verify the access token is still valid (invalidate, set to null if it isn't)
             * TODO: if any of the above fail, return defaultAccessToken.
             */
            return null;
        }
    }
}