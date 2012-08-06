using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
	public class PostListModelFromPostEntityEnumerableConverter : ITypeConverter<IEnumerable<Post>, PostListModel>
	{
		public PostListModel Convert(ResolutionContext context)
		{
			IEnumerable<Post> posts = (IEnumerable<Post>)context.SourceValue;
			PostListModel result = new PostListModel
			{
				Posts = posts.Select(AutoMapper.Mapper.Map<Post, PostModel>).ToList()
			};
			Post first = posts.FirstOrDefault();
			result.OpenGraph = AutoMapper.Mapper.Map<Post, OpenGraphModel>(first);
			return result;
		}
	}
}