using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Mvc.Model
{
	public class PostListFromEntitiesConverter : ITypeConverter<IEnumerable<Post>, PostListModel>
	{
		private readonly IMapper mapper;

		public PostListFromEntitiesConverter(IMapper mapper)
		{
			if (mapper == null)
			{
				throw new ArgumentNullException("mapper");
			}
			this.mapper = mapper;
		}

		public PostListModel Convert(ResolutionContext context)
		{
			IEnumerable<Post> posts = (IEnumerable<Post>)context.SourceValue;
			PostListModel result = new PostListModel
			{
				Posts = posts.Select(mapper.Map<Post, PostModel>).ToList()
			};
			Post first = posts.FirstOrDefault();
			result.OpenGraph = mapper.Map<Post, OpenGraphModel>(first);
			return result;
		}
	}
}