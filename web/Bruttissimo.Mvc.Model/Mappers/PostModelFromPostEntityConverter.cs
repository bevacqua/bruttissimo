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
			Link link = post.Link;
			if (link.Type == LinkType.Html)
			{
				return new LinkPostModel
				{
					Description = link.Description,
					PictureUrl = link.Picture,
					PostId = post.Id,
					PostSlug = postService.GetTitleSlug(post),
					Timestamp = post.Created,
					Title = link.Title,
					UserMessage = post.UserMessage,
					UserDisplayName = post.User.DisplayName
				};
			}
			else if (link.Type == LinkType.Image)
			{
				return new ImagePostModel
				{
					PictureUrl = link.Picture,
					PostId = post.Id,
					PostSlug = postService.GetTitleSlug(post),
					Timestamp = post.Created,
					UserMessage = post.UserMessage,
					UserDisplayName = post.User.DisplayName
				};
			}
			return null;
		}
	}
}