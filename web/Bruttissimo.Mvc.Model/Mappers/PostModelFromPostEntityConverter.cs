using System;
using AutoMapper;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
	public class PostModelFromPostEntityConverter : ITypeConverter<Post, PostModel>
	{
		private readonly IMapper mapper;

		public PostModelFromPostEntityConverter(IMapper mapper)
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
			switch (post.Link.Type)
			{
				default:
				{
					throw new ArgumentOutOfRangeException("post.Link.Type");
				}
				case LinkType.Html:
				{
					return mapper.Map<Post, LinkPostModel>(post);
				}
				case LinkType.Image:
				{
					return mapper.Map<Post, ImagePostModel>(post);
				}
			}
		}
	}
}