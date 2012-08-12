using System;
using System.Collections.Generic;
using System.Threading;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookRepository facebookRepository;

        public FacebookService(IFacebookRepository facebookRepository)
        {
            if (facebookRepository == null)
            {
                throw new ArgumentNullException("facebookRepository");
            }
            this.facebookRepository = facebookRepository;
        }

        public void Import()
        {
            Thread.Sleep(300000);
            string next;
            IList<FacebookPost> feed = facebookRepository.GetPostsInGroupFeed(null, out next);
        }
    }
}