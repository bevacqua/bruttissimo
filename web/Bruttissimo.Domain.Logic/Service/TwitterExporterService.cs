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
            throw new NotImplementedException("copy from exporter service for facebook");
        }
    }
}