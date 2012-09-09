using System;
using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class TwitterExporterService : ITwitterExporterService
    {
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
                TwitterPost result = twitterRepository.PostToFeed(post);

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
    }
}