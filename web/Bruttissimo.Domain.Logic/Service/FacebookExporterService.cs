using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookExporterService : ExporterService<FacebookPost>, IFacebookExporterService
    {
        private readonly IFacebookRepository fbRepository;
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;

        public FacebookExporterService(IFacebookRepository fbRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            Ensure.That(fbRepository, "fbRepository").IsNotNull();
            Ensure.That(postRepository, "postRepository").IsNotNull();
            Ensure.That(userRepository, "userRepository").IsNotNull();

            this.fbRepository = fbRepository;
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }

        public void Export(FacebookExportLog entry)
        {
            base.Export(entry);
        }

        protected override IList<Post> GetPostsToExport()
        {
            IList<Post> posts = postRepository.GetPostsPendingFacebookExport().ToList();
            return posts;
        }

        protected override FacebookPost Send(Post post)
        {
            string userAccessToken = GetUserAccessToken(post);
            FacebookPost response = fbRepository.PostToFeed(post, userAccessToken);
            return response;
        }

        protected override void Update(Post post, FacebookPost response)
        {
            post.FacebookPostId = response.Id;
            post.FacebookUserId = response.From.Id;
            post.FacebookFeedId = response.To.Data[0].Id;

            postRepository.Update(post);
        }

        internal string GetUserAccessToken(Post post)
        {
            if (post.User == null)
            {
                return null;
            }
            string accessToken = userRepository.GetFacebookAccessToken(post.User);

            bool valid = fbRepository.ValidateToken(accessToken);
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