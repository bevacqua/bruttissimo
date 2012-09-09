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
        private readonly IUserRepository userRepository;

        public FacebookExporterService(IFacebookRepository facebookRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            if (facebookRepository == null)
            {
                throw new ArgumentNullException("facebookRepository");
            }
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }
            if (userRepository== null)
            {
                throw new ArgumentNullException("userRepository");
            }
            this.facebookRepository = facebookRepository;
            this.postRepository = postRepository;
            this.userRepository = userRepository;
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
            string accessToken = userRepository.GetFacebookAccessToken(post.User);

            bool valid = facebookRepository.ValidateToken(accessToken);
            if (!valid)
            {
                userRepository.RevokeFacebookAccessToken(accessToken);
                return null;
            }
            else
            {
                return accessToken;
            }
        }
    }
}