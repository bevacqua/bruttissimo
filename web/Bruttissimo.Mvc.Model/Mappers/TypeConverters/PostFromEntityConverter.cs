using System;
using AutoMapper;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
    public class PostFromEntityConverter : ITypeConverter<Post, PostModel>
    {
        private readonly IMapper mapper;

        public PostFromEntityConverter(IMapper mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            this.mapper = mapper;
        }

        public PostModel Convert(ResolutionContext context)
        {
            Post post = (Post)context.SourceValue;
            PostModel result;

            switch (post.Link.Type)
            {
                default:
                {
                    throw new ArgumentOutOfRangeException("post.Link.Type");
                }
                case LinkType.Html:
                {
                    result = mapper.Map<Post, LinkPostModel>(post);
                    break;
                }
                case LinkType.Image:
                {
                    result = mapper.Map<Post, ImagePostModel>(post);
                    break;
                }
            }
            result.Comments = mapper.Map<Post, CommentListModel>(post);
            return result;
        }
    }
}
