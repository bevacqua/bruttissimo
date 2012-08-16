using System;

namespace Bruttissimo.Domain.Entity
{
	public class FacebookPost
	{
		public string Id { get; set; }
        public string FromId { get; set; }

        public string Type { get; set; } // TODO: enum??

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public string Message { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
		public string Description { get; set; }
	}
}