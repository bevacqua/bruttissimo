using System;
using System.Web;
using Bruttissimo.Common.Mvc.Extensions;

namespace Bruttissimo.Mvc.Model.ViewModels
{
    public abstract class PostModel
    {
        public long Id { get; set; }
        public string PostSlug { get; set; }

        public DateTime Created { get; set; }
        public string UserDisplayName { get; set; }

        public IHtmlString UserMessage { get; set; }

        public bool HasUserMessage
        {
            get { return !UserMessage.NullOrEmpty(); }
        }

        public CommentListModel Comments { get; set; }
    }
}
