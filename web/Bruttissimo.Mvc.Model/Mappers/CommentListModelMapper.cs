using System.Collections.Generic;
using System.Linq;
using Bruttissimo.Common.Interface;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Mvc.Model.ViewModels;

namespace Bruttissimo.Mvc.Model.Mappers
{
    public class CommentListModelMapper : IMapperConfigurator
    {
        public void CreateMaps(IMapper mapper)
        {
            mapper.CreateMap<Domain.Entity.Entities.Post, CommentListModel>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Form, opt => opt.MapFrom(src => new CommentReplyModel(src.Id, null)))
                .ForMember(dest => dest.Threads, opt => opt.MapFrom(src => GetCommentThreads(src, mapper)));
        }

        private IList<CommentThreadModel> GetCommentThreads(Domain.Entity.Entities.Post post, IMapper mapper)
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