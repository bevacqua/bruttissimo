using System.Collections.Generic;

namespace Bruttissimo.Mvc.Model
{
    public class CommentModel
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Message { get; set; }

        public IList<CommentModel> Replies { get; set; }

        public bool HasReplies
        {
            get { return Replies.Count > 0; }
        }
    }
}
