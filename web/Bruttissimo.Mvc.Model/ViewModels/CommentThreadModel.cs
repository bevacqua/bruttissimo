using System.Collections.Generic;

namespace Bruttissimo.Mvc.Model.ViewModels
{
    public class CommentThreadModel
    {
        public CommentModel Original { get; set; }
        public IList<CommentModel> Replies { get; set; }

        public CommentReplyModel Form { get; set; }
    }
}