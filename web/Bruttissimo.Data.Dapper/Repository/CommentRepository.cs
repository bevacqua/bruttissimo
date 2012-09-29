using System;
using System.Collections.Generic;
using System.Data;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Dapper;

namespace Bruttissimo.Data.Dapper
{
    public class CommentRepository : EntityRepository<Comment>, ICommentRepository
    {
        private readonly IDbConnection connection;

        public CommentRepository(IDbConnection connection)
            : base(connection)
        {
            Ensure.That(connection, "connection").IsNotNull();

            this.connection = connection;
        }

        public IEnumerable<Comment> GetByPostId(long postId)
        {
            const string sql = @"
					SELECT [Comment].*
					FROM [Comment]
					LEFT JOIN [Post] ON [Comment].[PostId] = [Post].[Id]
					WHERE [Post].[Id] = @postId
				";
            IEnumerable<Comment> comments = connection.Query<Comment>(sql, new { postId });
            return comments;
        }
    }
}
