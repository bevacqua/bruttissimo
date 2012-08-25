using System;
using System.Data;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;

namespace Bruttissimo.Data.Dapper
{
    public class CommentRepository : EntityRepository<Comment>, ICommentRepository
    {
        private readonly IDbConnection connection;

        public CommentRepository(IDbConnection connection)
            : base(connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            this.connection = connection;
        }
    }
}
