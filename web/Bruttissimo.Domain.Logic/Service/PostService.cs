using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
	public class PostService : IPostService
	{
		private readonly IPostRepository postRepository;
		private readonly ILinkRepository linkRepository;
		private readonly TextHelper textHelper;

		public PostService(IPostRepository postRepository, ILinkRepository linkRepository, TextHelper textHelper)
		{
			if (postRepository == null)
			{
				throw new ArgumentNullException("postRepository");
			}
			if (linkRepository == null)
			{
				throw new ArgumentNullException("linkRepository");
			}
			if (textHelper == null)
			{
				throw new ArgumentNullException("textHelper");
			}
			this.postRepository = postRepository;
			this.linkRepository = linkRepository;
			this.textHelper = textHelper;
		}

		public Post GetById(long id, bool includeLink = true)
		{
			Post post = postRepository.GetById(id, includeLink);
			return post;
		}

		public IEnumerable<Post> GetLatest(long? timestamp, int count)
		{
			DateTime? until = null;
			if (timestamp.HasValue)
			{
				until = DateTime.FromBinary(timestamp.Value);
			}
			IEnumerable<Post> posts = postRepository.GetLatest(until, count);
			return posts;
		}

		public string GetTitleSlug(Post post)
		{
			if (post.Link == null)
			{
				return textHelper.Slugify(post.UserMessage, 40);
			}
			else
			{
				return textHelper.Slugify(post.Link.Title);
			}
		}

		public Post Create(Link link, string message, User user)
		{
			Post post = postRepository.Insert(link, message, user);
			return post;
		}
	}
}