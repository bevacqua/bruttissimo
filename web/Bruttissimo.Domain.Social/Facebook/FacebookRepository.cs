using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;
using Facebook;
using Newtonsoft.Json;
using log4net;

namespace Bruttissimo.Domain.Social
{
    public class FacebookRepository : IFacebookRepository
    {
        private const int PAGE_LIMIT = 15;
        private const string GRAPH_FEED_LIMITED = "{0}/feed?limit={1}&fields=id,from,to,type,created_time,updated_time,message,link,name,caption,description";
        private const string GRAPH_FEED_SINCE = "{0}&since={1}";

        private const string DEBUG_API_GET = "Facebook API GET: {0}";

        private readonly ILog log = LogManager.GetLogger(typeof(FacebookRepository));

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

        public IList<FacebookPost> GetPostsInFeed(string feed, DateTime? since)
        {
            if (feed == null)
            {
                throw new ArgumentNullException("feed");
            }
            string url = GRAPH_FEED_LIMITED.FormatWith(feed, PAGE_LIMIT);

            if (since.HasValue)
            {
                string date = since.Value.ToIso8601();
                url = GRAPH_FEED_SINCE.FormatWith(url, date);
            }

            IList<FacebookPost> result = FetchAll(url);
            return result;
        }

        internal IList<FacebookPost> FetchAll(string url)
        {
            List<FacebookPost> posts = new List<FacebookPost>();
            do
            {
                FacebookPostCollection response = Fetch(url);
                posts.AddRange(response.Data);

                if (/*response.Data.Count < PAGE_LIMIT ||*/ response.Paging.Next == url) // sanity
                {
                    break;
                }
                url = response.Paging.Next;
            } while (url != null);

            return posts;
        }

        internal FacebookPostCollection Fetch(string url)
        {
            log.Debug(DEBUG_API_GET.FormatWith(url));
            FacebookClient client = new FacebookClient(defaultAccessToken);
            string json = (client.Get(url) ?? string.Empty).ToString();

            FacebookPostCollection response = JsonConvert.DeserializeObject<FacebookPostCollection>(json);
            return response;
        }
    }
}
