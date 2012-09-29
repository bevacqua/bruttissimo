using System;
using System.Collections.Generic;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Domain.Logic
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            Ensure.That(commentRepository, "commentRepository").IsNotNull();

            this.commentRepository = commentRepository;
        }

        public Comment Create(long postId, string message, User user, long? parentId)
        {
            Ensure.That(message, "message").IsNotNull();
            Ensure.That(user, "user").IsNotNull();

            if (parentId.HasValue)
            {
                // prevent nesting deeper than one level.
                Comment parent = commentRepository.GetById(parentId.Value);
                Ensure.That(() => parent == null || parent.ParentId.HasValue).IsFalseOrThrow<ArgumentOutOfRangeException>();
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
