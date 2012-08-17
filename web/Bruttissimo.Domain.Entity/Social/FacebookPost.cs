using System;
using Newtonsoft.Json;

namespace Bruttissimo.Domain.Entity
{
	public class FacebookPost
	{
		public string Id { get; set; }

        [JsonProperty("to.data.id")]
        public string FeedId { get; set; }

        [JsonProperty("from.id")]
        public string UserId { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("updated_time")]
        public DateTime UpdatedTime { get; set; }

        public string Type { get; set; } // TODO: Type enum??

        public string Message { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
		public string Description { get; set; }
	}
}