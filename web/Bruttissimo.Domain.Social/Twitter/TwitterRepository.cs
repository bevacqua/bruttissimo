using System;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;
using TweetSharp;

namespace Bruttissimo.Domain.Social
{
    public class TwitterRepository : ITwitterRepository
    {
        private readonly TwitterServiceParams defaultServiceParams;
        private readonly IMapper mapper;

        public TwitterRepository(TwitterServiceParams defaultServiceParams, IMapper mapper)
        {
            if (defaultServiceParams == null)
            {
                throw new ArgumentNullException("defaultServiceParams");
            }
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.defaultServiceParams = defaultServiceParams;
            this.mapper = mapper;
        }

        private TwitterService InstanceTwitterService(TwitterServiceParams parameters)
        {
            string app = parameters.App;
            string appSecret = parameters.AppSecret;
            string token = parameters.Token;
            string tokenSecret = parameters.TokenSecret;

            TwitterService service = new TwitterService();
            service.AuthenticateWith(app, appSecret, token, tokenSecret);
            return service;
        }

        public TwitterPost PostToFeed(string message, TwitterServiceParams serviceParams = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            TwitterServiceParams parameters = serviceParams ?? defaultServiceParams;
            TwitterService service = InstanceTwitterService(parameters);
            TwitterStatus status = service.SendTweet(message);
            TwitterPost post = mapper.Map<TwitterStatus, TwitterPost>(status);

            return post;
        }
    }
}
