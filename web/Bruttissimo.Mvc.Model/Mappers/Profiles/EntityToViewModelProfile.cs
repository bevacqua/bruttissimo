using System;
using System.Collections.Generic;
using AutoMapper;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
	public class EntityToViewModelProfile : Profile
	{
		private readonly IPostService postService;

		public EntityToViewModelProfile(IPostService postService)
		{
			if (postService == null)
			{
				throw new ArgumentNullException("postService");
			}
			this.postService = postService;
		}

		protected override void Configure()
		{
			CreateMap<Post, PostModel>().ConvertUsing<PostModelFromPostEntityConverter>();
			CreateMap<Post, LinkPostModel>();
			CreateMap<Post, ImagePostModel>();

			//return new LinkPostModel
			//{
			//    Description = link.Description,
			//    PictureUrl = link.Picture,
			//    PostId = post.Id,
			//    PostSlug = postService.GetTitleSlug(post),
			//    Timestamp = post.Created,
			//    Title = link.Title,
			//    UserMessage = post.UserMessage,
			//    UserDisplayName = post.User.DisplayName
			//};
			//return new ImagePostModel
			//{
			//    PictureUrl = link.Picture,
			//    PostId = post.Id,
			//    PostSlug = postService.GetTitleSlug(post),
			//    Timestamp = post.Created,
			//    UserMessage = post.UserMessage,
			//    UserDisplayName = post.User.DisplayName
			//};

			CreateMap<Post, OpenGraphModel>();
			CreateMap<IEnumerable<Post>, PostListModel>().ConvertUsing<PostListModelFromPostEntityEnumerableConverter>();
		}
	}
}