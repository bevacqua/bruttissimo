using System;
using System.Collections.Generic;
using Bruttissimo.Common;
using Bruttissimo.Common.Guard;
using Bruttissimo.Common.Utility;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Bruttissimo.Domain.Service;

namespace Bruttissimo.Domain.Logic.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly TextHelper textHelper;
        private readonly ICommentService commentService;

        public PostService(IPostRepository postRepository, ICommentService commentService, TextHelper textHelper)
        {
            Ensure.That(postRepository, "postRepository").IsNotNull();
            Ensure.That(commentService, "commentService").IsNotNull();
            Ensure.That(textHelper, "textHelper").IsNotNull();

            this.postRepository = postRepository;
            this.commentService = commentService;
            this.textHelper = textHelper;
        }

        public Post GetById(long id, bool includeLink = false, bool includeComments = false)
        {
            Post post = postRepository.GetById(id, includeLink);
            if (includeComments)
            {
                post.Comments = commentService.GetComments(post);
            }
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
