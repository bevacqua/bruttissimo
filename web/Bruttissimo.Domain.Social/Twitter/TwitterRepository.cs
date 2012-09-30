using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Interface;
using Bruttissimo.Domain.DTO.Twitter;
using Bruttissimo.Domain.Entity.Social.Twitter;
using Bruttissimo.Domain.Repository.Social;
using TweetSharp;

namespace Bruttissimo.Domain.Social.Twitter
{
    public class TwitterRepository : ITwitterRepository
    {
        private readonly TwitterServiceParams defaultServiceParams;
        private readonly IMapper mapper;

        public TwitterRepository(TwitterServiceParams defaultServiceParams, IMapper mapper)
        {
            Ensure.That(() => defaultServiceParams).IsNotNull();
            Ensure.That(() => mapper).IsNotNull();

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
            Ensure.That(() => message).IsNotNull();

            TwitterServiceParams parameters = serviceParams ?? defaultServiceParams;
            TwitterService service = InstanceTwitterService(parameters);
            TwitterStatus status = service.SendTweet(message);
            TwitterPost post = mapper.Map<TwitterStatus, TwitterPost>(status);

            return post;
        }
    }
}
