using System;
using AutoMapper;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
	public class PostModelFromPostEntityConverter : ITypeConverter<Post, PostModel>
	{
		private readonly IPostService postService;

		public PostModelFromPostEntityConverter(IPostService postService)
		{
			if (postService == null)
			{
				throw new ArgumentNullException("postService");
			}
			this.postService = postService;
		}

		public PostModel Convert(ResolutionContext context)
		{
			Post post = (Post)context.SourceValue;
			switch (post.Link.Type)
			{
				default:
				{
					throw new ArgumentOutOfRangeException("link.Type");
				}
				case LinkType.Html:
				{
					return Mapper.Map<Post, LinkPostModel>(post);
				}
				case LinkType.Image:
				{
					return Mapper.Map<Post, ImagePostModel>(post);
				}
			}
		}
	}
}