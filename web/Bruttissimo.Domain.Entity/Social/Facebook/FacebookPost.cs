using System;
using Newtonsoft.Json;

namespace Bruttissimo.Domain.Entity
{
    public class FacebookPost
    {
        public string Id { get; set; }

        public FacebookTo To { get; set; }
        public FacebookFrom From { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("updated_time")]
        public DateTime UpdatedTime { get; set; }

        [JsonConverter(typeof(FacebookPostTypeEnumConverter))]
        public FacebookPostType Type { get; set; }

        public string Message { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
