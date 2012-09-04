using System.Collections.Generic;
using Bruttissimo.Common;

namespace Bruttissimo.Mvc.Model
{
    public class CommentModel
    {
        public long Id { get; set; }
        public string Message { get; set; }
    }

    public class CommentListModel
    {
        public long PostId { get; set; }

        public IList<CommentThreadModel> Threads { get; set; }
        public CommentReplyModel Form { get; set; }
    }

    public class CommentThreadModel
    {
        public CommentModel Original { get; set; }
        public IList<CommentModel> Replies { get; set; }

        public CommentReplyModel Form { get; set; }
    }

    public class CommentReplyModel
    {
        public string Name { get; private set; }
        public long PostId { get; private set; }
        public long? ParentId { get; private set; }

        public CommentReplyModel(long postId, long? parentId = null)
        {
            PostId = postId;
            ParentId = parentId;
            Name = GenerateName();
        }

        internal string GenerateName()
        {
            if (ParentId.HasValue)
            {
                return "comment-for-{0}-{1}".FormatWith(PostId, ParentId.Value);
            }
            else
            {
                return "comment-for-{0}".FormatWith(PostId);
            }
        }
    }
}
