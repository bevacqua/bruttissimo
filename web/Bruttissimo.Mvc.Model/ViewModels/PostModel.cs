using System;
using Bruttissimo.Common;

namespace Bruttissimo.Mvc.Model
{
    public abstract class PostModel
	{
		public long Id { get; set; }
        public string PostSlug { get; set; }

        public DateTime Created { get; set; }
        public string UserDisplayName { get; set; }

        public string UserMessage { get; set; }
        public bool HasUserMessage { get { return !UserMessage.NullOrEmpty(); } }
    }
}