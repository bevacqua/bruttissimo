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
        private const int PAGE_LIMIT = 15;
        private const string GRAPH_FEED_LIMITED = "{0}/feed?limit={1}";
        private const string GRAPH_FEED_SINCE = "{0}&since={1}";

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

        public IEnumerable<FacebookPost> GetPostsInGroupFeed(string group, DateTime? since)
        {
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            string feed = GRAPH_FEED_LIMITED.FormatWith(group, PAGE_LIMIT);
            return GetPostsInFeed(feed, since);
        }

        public IEnumerable<FacebookPost> GetPostsInFeed(string url, DateTime? since)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            if (since.HasValue)
            {
                string date = since.Value.ToIso8601();
                url = GRAPH_FEED_SINCE.FormatWith(url, date);
            }
            FacebookClient client = new FacebookClient(defaultAccessToken);
            FacebookPostCollection response;
            List<FacebookPost> posts = new List<FacebookPost>();
            do
            {
                string json = (client.Get(url) ?? string.Empty).ToString();
                response = JsonConvert.DeserializeObject<FacebookPostCollection>(json);

                posts.AddRange(response.Data);

                if (response.Paging.Next == null || response.Paging.Next == url) // sanity
                {
                    break;
                }
                url = response.Paging.Next;
            } while (response.Data.Count < PAGE_LIMIT);

            return posts;
        }
    }
}
