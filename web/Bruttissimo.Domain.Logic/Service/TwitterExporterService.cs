using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class TwitterExporterService : ExporterService<TwitterPost>, ITwitterExporterService
    {
        private const int MAX_TWEET_LENGTH = 140;

        private readonly ITwitterRepository twitterRepository;
        private readonly IPostRepository postRepository;
        private readonly IUrlShortener urlShortener;

        public TwitterExporterService(ITwitterRepository twitterRepository, IPostRepository postRepository, IUrlShortener urlShortener)
        {
            Ensure.That(twitterRepository, "twitterRepository").IsNotNull();
            Ensure.That(postRepository, "postRepository").IsNotNull();
            Ensure.That(urlShortener, "urlShortener").IsNotNull();

            this.twitterRepository = twitterRepository;
            this.postRepository = postRepository;
            this.urlShortener = urlShortener;
        }

        public void Export(TwitterExportLog entry)
        {
            base.Export(entry);
        }

        protected override IList<Post> GetPostsToExport()
        {
            IList<Post> posts = postRepository.GetPostsPendingTwitterExport().ToList();
            return posts;
        }

        protected override TwitterPost Send(Post post)
        {
            string status = GetStatusMessageForPost(post);
            TwitterPost response = twitterRepository.PostToFeed(status);
            return response;
        }

        protected override void Update(Post post, TwitterPost response)
        {
            post.TwitterPostId = response.Id;
            post.TwitterUserId = response.FromId;

            postRepository.Update(post);
        }

        private string GetStatusMessageForPost(Post post)
        {
            StringBuilder status = new StringBuilder();
            string longUrl = urlHelper.PublicRouteUrl("PostShortcut", new { id = post.Id });
            string url = urlShortener.Shorten(longUrl);

            string description = post.UserMessage ?? post.Link.Title ?? Common.Resources.User.TwitterLink;
            string message = description.Trim() + " ";

            int maxlen = MAX_TWEET_LENGTH - url.Length;
            if (maxlen > 4)
            {
                if (message.Length > maxlen)
                {
                    status.Append(message.Substring(0, maxlen - 3));
                    status.Append(".. ");
                }
                else
                {
                    status.Append(message);
                }
            }
            status.Append(url);

            return status.ToString();
        }
    }
}