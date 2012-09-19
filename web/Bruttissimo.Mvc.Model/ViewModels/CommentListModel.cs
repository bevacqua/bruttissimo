using System.Collections.Generic;

namespace Bruttissimo.Mvc.Model
{
    public class CommentListModel
    {
        public long PostId { get; set; }

        public IList<CommentThreadModel> Threads { get; set; }
        public CommentReplyModel Form { get; set; }
    }
}