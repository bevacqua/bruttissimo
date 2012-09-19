using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class TwitterExporterService : BaseService, ITwitterExporterService
    {
        private const int MAX_TWEET_LENGTH = 140;

        private readonly ITwitterRepository twitterRepository;
        private readonly IPostRepository postRepository;
        private readonly IUrlShortener urlShortener;

        public TwitterExporterService(ITwitterRepository twitterRepository, IPostRepository postRepository, IUrlShortener urlShortener)
        {
            if (twitterRepository == null)
            {
                throw new ArgumentNullException("twitterRepository");
            }
            if (postRepository == null)
            {
                throw new ArgumentNullException("postRepository");
            }
            if (urlShortener == null)
            {
                throw new ArgumentNullException("urlShortener");
            }
            this.twitterRepository = twitterRepository;
            this.postRepository = postRepository;
            this.urlShortener = urlShortener;
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