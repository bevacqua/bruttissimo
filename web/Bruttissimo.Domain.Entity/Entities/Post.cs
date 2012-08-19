using System;
using Newtonsoft.Json;

namespace Bruttissimo.Domain.Entity
{
	public class Post
	{
        [JsonIgnore]
		public long Id { get; set; }

        [JsonIgnore]
        public long? UserId { get; set; }
        [JsonIgnore]
		public User User { get; set; }

        [JsonIgnore]
        public long? LinkId { get; set; }
        [JsonIgnore]
		public Link Link { get; set; }

		public string UserMessage { get; set; }

        public DateTime Created { get; set; }

        [JsonIgnore]
		public DateTime? Updated { get; set; }

        // Facebook specific properties.
        public string FacebookFeedId { get; set; }
        public string FacebookPostId { get; set; }
        public string FacebookUserId { get; set; }
	}
}
