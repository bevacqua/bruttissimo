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

        /// <summary>
        /// Access token used when no user-specific access token is provided to a method.
        /// </summary>
        private readonly string defaultAccessToken;
        
        public FacebookRepository(string defaultAccessToken)
        {
            if (defaultAccessToken == null)
            {
                throw new ArgumentNullException("defaultAccessToken");
            }
            this.defaultAccessToken = defaultAccessToken;
        }

        public IList<FacebookPost> GetPostsInGroupFeed(string group, out string next)
        {
            string feed = GRAPH_FEED_LIMITED.FormatWith(group, DEFAULT_PAGE_LIMIT);
            return GetPostsInFeed(feed, out next);
        }

        public IList<FacebookPost> GetPostsInFeed(string url, out string next)
        {
            FacebookClient client = new FacebookClient(defaultAccessToken);
            string json = (client.Get(url) ?? string.Empty).ToString();
            FacebookPostCollection response = JsonConvert.DeserializeObject<FacebookPostCollection>(json);
            next = response.Paging.Next;
            return response.Data;
        }
    }
}
