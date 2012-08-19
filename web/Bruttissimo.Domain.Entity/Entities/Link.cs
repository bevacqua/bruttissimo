using System;
using Newtonsoft.Json;

namespace Bruttissimo.Domain.Entity
{
	public class Link
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonIgnore]
		public LinkType Type { get; set; }

		public string ReferenceUri { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Picture { get; set; }

        [JsonIgnore]
		public DateTime Created { get; set; }

        [JsonIgnore]
		public long? PostId;
	}
}