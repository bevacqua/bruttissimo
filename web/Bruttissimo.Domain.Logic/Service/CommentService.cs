using System;
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

        public Comment Create(long postId, string message, User user)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            Comment comment = new Comment
            {
                PostId = postId,
                Message = message,
                Created = DateTime.UtcNow,
                UserId = user.Id
            };
            return commentRepository.Insert(comment);
        }
    }
}
