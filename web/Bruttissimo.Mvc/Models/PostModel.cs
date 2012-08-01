using System;
using Bruttissimo.Common;

namespace Bruttissimo.Mvc.Models
{
    public abstract class PostModel
	{
		public long PostId { get; set; }
        public string PostSlug { get; set; }

        public DateTime Timestamp { get; set; }
        public string UserDisplayName { get; set; }

        public string UserMessage { get; set; }
        public bool HasUserMessage { get { return !UserMessage.NullOrEmpty(); } }
    }
}