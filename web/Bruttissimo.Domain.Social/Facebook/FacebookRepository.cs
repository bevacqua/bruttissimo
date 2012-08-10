using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;
using Facebook;
using Newtonsoft.Json;

namespace Bruttissimo.Domain.Social
{
    public class FacebookRepository : IFacebookRepository
    {
        private const int DEFAULT_PAGE_LIMIT = 15;
        private const string GRAPH_FEED_LIMITED = "{0}/feed?limit={1}";

        private readonly string accessToken;

        public FacebookRepository(string accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException("accessToken");
            }
            this.accessToken = accessToken;
        }

        public IList<FacebookPost> GetPostsInGroupFeed(string group, out string next)
        {
            string feed = GRAPH_FEED_LIMITED.FormatWith(group, DEFAULT_PAGE_LIMIT);
            return GetPostsInFeed(feed, out next);
        }

        public IList<FacebookPost> GetPostsInFeed(string url, out string next)
        {
            FacebookClient client = new FacebookClient(accessToken);
            string json = (client.Get(url) ?? string.Empty).ToString();
            FacebookPostCollection response = JsonConvert.DeserializeObject<FacebookPostCollection>(json);
            next = response.Paging.Next;
            return response.Data;
        }
    }
}
