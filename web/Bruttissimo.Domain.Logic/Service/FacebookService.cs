using System;
using System.Collections.Generic;
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
            string next;
            IList<FacebookPost> feed = facebookRepository.GetPostsInGroupFeed(null, out next);

        }
    }
}