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

			CreateMap<Post, LinkPostModel>().ForMember(
				m => m.PostSlug,
				x => x.MapFrom(p => postService.GetTitleSlug(p))
			);
			CreateMap<Post, ImagePostModel>().ForMember(
				m => m.PostSlug,
				x => x.MapFrom(p => postService.GetTitleSlug(p))
			);

			CreateMap<Post, OpenGraphModel>();
			CreateMap<Link, LinkModel>();

			CreateMap<IEnumerable<Post>, PostListModel>().ConvertUsing<PostListModelFromPostEntityEnumerableConverter>();
		}
	}
}