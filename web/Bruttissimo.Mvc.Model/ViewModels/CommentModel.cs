using System.Collections.Generic;

namespace Bruttissimo.Mvc.Model
{
    public class CommentModel
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Message { get; set; }

        public IList<CommentModel> Comments { get; set; }

        public bool HasComments
        {
            get { return Comments.Count > 0; }
        }
    }
}
