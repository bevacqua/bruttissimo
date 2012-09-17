using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class CommentListModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Post, CommentListModel>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Form, opt => opt.MapFrom(src => new CommentReplyModel(src.Id)))
                .ForMember(dest => dest.Threads, opt => opt.MapFrom(src => GetCommentThreads(src, mapper)));
        }

        private IList<CommentThreadModel> GetCommentThreads(Post post, IMapper mapper)
        {
            if (post.Comments == null) // sanity
            {
                return new List<CommentThreadModel>();
            }
            IEnumerable<Comment> roots = post.Comments.Where(comment => !comment.ParentId.HasValue);
            IList<CommentThreadModel> threads = roots.Select(comment => new CommentThreadModel
            {
                Original = mapper.Map<Comment, CommentModel>(comment),
                Replies = mapper.Map<IEnumerable<Comment>, IList<CommentModel>>(post.Comments.Where(c => c.ParentId == comment.Id)),
                Form = new CommentReplyModel(post.Id, comment.Id)
            }).ToList();

            return threads;
        }
    }
}