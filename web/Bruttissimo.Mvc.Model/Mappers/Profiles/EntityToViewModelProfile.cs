using System.Collections.Generic;
using AutoMapper;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
	public class EntityToViewModelProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Post, PostModel>().ConvertUsing<PostModelFromPostEntityConverter>();
			CreateMap<IEnumerable<Post>, PostListModel>().ConvertUsing<PostListModelFromPostEntityEnumerableConverter>();
		}
	}
}