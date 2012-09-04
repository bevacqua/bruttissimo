using System;
using System.Collections.Generic;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            if (commentRepository == null)
            {
                throw new ArgumentNullException("commentRepository");
            }
            this.commentRepository = commentRepository;
        }

        public Comment Create(long postId, string message, User user, long? parentId)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (parentId.HasValue)
            {
                Comment parent = commentRepository.GetById(parentId.Value);
                if (parent == null || parent.ParentId.HasValue) // prevent nesting deeper than one level.
                {
                    throw new ArgumentOutOfRangeException("parentId");
                }
            }
            Comment comment = new Comment
            {
                PostId = postId,
                Message = message,
                Created = DateTime.UtcNow,
                UserId = user.Id,
                ParentId = parentId
            };
            return commentRepository.Insert(comment);
        }

        public IEnumerable<Comment> GetComments(Post post)
        {
            IEnumerable<Comment> comments = commentRepository.GetByPostId(post.Id);
            return comments;
        }
    }
}
