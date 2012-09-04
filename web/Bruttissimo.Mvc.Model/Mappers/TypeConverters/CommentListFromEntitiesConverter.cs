using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class CommentListFromEntitiesConverter : ITypeConverter<Post, CommentListModel>
    {
        private readonly IMapper mapper;

        public CommentListFromEntitiesConverter(IMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.mapper = mapper;
        }

        public CommentListModel Convert(ResolutionContext context)
        {
            Post post = (Post)context.SourceValue;
            CommentListModel model = new CommentListModel
            {
                PostId = post.Id,
                Form = new CommentReplyModel(post.Id),
                Threads = Enumerable.Empty<CommentThreadModel>().ToList()
            };
            if (post.Comments == null) // sanity
            {
                return model;
            }

            IEnumerable<Comment> roots = post.Comments.Where(c => !c.ParentId.HasValue);
            IList<CommentThreadModel> threads = roots.Select(comment => new CommentThreadModel
            {
                Original = mapper.Map<Comment, CommentModel>(comment),
                Replies = mapper.Map<IEnumerable<Comment>, IList<CommentModel>>(post.Comments.Where(c => c.ParentId == comment.Id)),
                Form = new CommentReplyModel(post.Id, comment.Id)
            }).ToList();

            model.Threads = threads;
            return model;
        }
    }
}
