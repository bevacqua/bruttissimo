using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class TwitterExporterService : BaseService, ITwitterExporterService
    {
        private const int MAX_TWEET_LENGTH = 140;

        private readonly ITwitterRepository twitterRepository;
        private readonly IPostRepository postRepository;

        public TwitterExporterService(ITwitterRepository twitterRepository, IPostRepository postRepository)
        {
            if (twitterRepository == null)
            {
                throw new ArgumentNullException("twitterRepository");
            }
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }
            this.twitterRepository = twitterRepository;
            this.postRepository = postRepository;
        }

        public void Export(TwitterExportLog entry)
        {
            IList<Post> posts = postRepository.GetPostsPendingTwitterExport().ToList();

            int exportCount = 0;

            foreach (Post post in posts)
            {
                string status = GetStatusMessageForPost(post);
                TwitterPost result = twitterRepository.PostToFeed(status);

                if (result == null) // post failed.
                {
                    continue;
                }
                post.TwitterPostId = result.Id;
                post.TwitterUserId = result.FromId;

                postRepository.Update(post);
                exportCount++;
            }
            entry.ExportCount = exportCount;
            entry.PostCount = posts.Count;
        }

        private string GetStatusMessageForPost(Post post)
        {
            StringBuilder status = new StringBuilder();
            string url = urlHelper.Action("Details", "Posts", new { id = post.Id }, "http");
            string message = post.UserMessage ?? post.Link.Title ?? Common.Resources.User.TwitterLink;

            int maxlen = MAX_TWEET_LENGTH - url.Length;
            if (maxlen > 4)
            {
                if (message.Length < maxlen - 1) // take the space into account.
                {
                    message = message.Substring(0, maxlen - 3) + "...";
                }
                status.Append(message);
                status.Append(" ");
            }
            status.Append(url);

            return status.ToString();
        }
    }
}