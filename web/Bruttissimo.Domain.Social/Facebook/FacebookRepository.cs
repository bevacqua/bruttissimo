using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        private const string GRAPH_FEED = "{0}/feed";
        private const string GRAPH_FEED_LIMITED = "{0}/feed?limit={1}";
        private const string GRAPH_FEED_SINCE = "{0}&since={1}";

        private const string DEBUG_API_GET = "Facebook API GET: {0}";
        private const string INVALID_ACCESS_TOKEN_TESTED = "Invalid access token: {0}";

        private readonly ILog log = LogManager.GetLogger(typeof(FacebookRepository));

        /// <summary>
        /// Access token used when no user-specific access token is provided to a method.
        /// </summary>
        private readonly string defaultAccessToken;
        private readonly IMapper mapper;

        public FacebookRepository(string defaultAccessToken, IMapper mapper)
        {
            if (defaultAccessToken == null)
            {
                throw new ArgumentNullException("defaultAccessToken");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.defaultAccessToken = defaultAccessToken;
            this.mapper = mapper;
        }

        public IList<FacebookPost> GetPostsInFeed(FacebookImportOptions opts)
        {
            if (opts == null)
            {
                throw new ArgumentNullException("opts");
            }
            DateTime? since = opts.Since;
            string url = GRAPH_FEED_LIMITED.FormatWith(opts.Feed, PAGE_LIMIT);

            if (since.HasValue)
            {
                string date = since.Value.ToIso8601();
                url = GRAPH_FEED_SINCE.FormatWith(url, date);
            }

            IList<FacebookPost> result = FetchAll(url, since, opts.Log);
            return result;
        }

        public FacebookPost PostToFeed(Post post, string userAccessToken)
        {
            if (post == null)
            {
                throw new ArgumentNullException("post");
            }
            if (post.FacebookFeedId == null) // sanity
            {
                return null;
            }
            string feed = GRAPH_FEED.FormatWith(post.FacebookFeedId);

            // create Facebook Post JSON object.
            FacebookPost facebookPost = mapper.Map<Post, FacebookPost>(post);
            string json = JsonConvert.SerializeObject(facebookPost);

            string accessToken = userAccessToken ?? defaultAccessToken;
            FacebookClient client = new FacebookClient(accessToken);

            // deserialize and return response.
            dynamic response = client.Post(feed, json);
            FacebookPost deserialized = JsonConvert.DeserializeObject<FacebookPost>(response);
            return deserialized;
        }

        public bool ValidateToken(string accessToken)
        {
            if (accessToken == null)
            {
                return false;
            }
            FacebookClient client = new FacebookClient(accessToken);
            try
            {
                client.Get("me");
            }
            catch (FacebookOAuthException)
            {
                log.Info(INVALID_ACCESS_TOKEN_TESTED.FormatWith(accessToken));
                return false;
            }
            return true;
        }

        internal IList<FacebookPost> FetchAll(string url, DateTime? since, FacebookImportLog importLog)
        {
            int queryCount = 0;
            List<FacebookPost> posts = new List<FacebookPost>();
            do
            {
                FacebookPostCollection response = Fetch(url);
                posts.AddRange(response.Data);

                queryCount++;

                if (since.HasValue && response.Data.Any(p => p.UpdatedTime <= since.Value)) // prevent unnecessary over-querying.
                {
                    posts.RemoveAll(p => p.UpdatedTime <= since.Value); // removed already-evaluated posts.
                    break;
                }
                if (response.Paging == null || response.Paging.Next == url) // sanity
                {
                    break;
                }
                url = response.Paging.Next;
            } while (url != null);

            importLog.QueryCount = queryCount;
            importLog.PostCount = posts.Count;
            importLog.PostUpdated = posts.MaxOrDefault(p => (DateTime?)p.UpdatedTime);
            return posts;
        }

        internal FacebookPostCollection Fetch(string url)
        {
            LogApiCall(url);

            FacebookClient client = new FacebookClient(defaultAccessToken);
            string json = (client.Get(url) ?? string.Empty).ToString();

            FacebookPostCollection response = JsonConvert.DeserializeObject<FacebookPostCollection>(json);
            return response;
        }

        private const string FACEBOOK_GRAPH_API = "https://graph.facebook.com/";
        private const string FACEBOOK_ACCESS_TOKEN_REGEX = "&access_token=(?:[^&])+";

        private void LogApiCall(string url)
        {
            string parameters = url;
            if (parameters.StartsWith(FACEBOOK_GRAPH_API))
            {
                parameters = parameters.Remove(0, FACEBOOK_GRAPH_API.Length);
            }
            parameters = Regex.Replace(parameters, FACEBOOK_ACCESS_TOKEN_REGEX, string.Empty);
            log.Debug(DEBUG_API_GET.FormatWith(parameters));
        }
    }
}
