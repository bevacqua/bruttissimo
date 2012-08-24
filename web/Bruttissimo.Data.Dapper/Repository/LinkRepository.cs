using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Dapper;

namespace Bruttissimo.Data.Dapper
{
    public class LinkRepository : EntityRepository<Link>, ILinkRepository
    {
        private readonly IDbConnection connection;

        public LinkRepository(IDbConnection connection)
            : base(connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            this.connection = connection;
        }

        public Link GetByReferenceUri(Uri referenceUri)
        {
            if (referenceUri == null)
            {
                throw new ArgumentNullException("referenceUri");
            }
            const string sql = @"
				SELECT [Link].*, [Post].[Id] AS [PostId]
				FROM [Link]
				LEFT JOIN [Post] ON [Post].[LinkId] = [Link].[Id]
				WHERE [Link].[ReferenceUri] = @referenceUri
            ";
            IEnumerable<Link> result = connection.Query<Link>(sql, new {referenceUri = referenceUri.AbsoluteUri});

            Link link = result.FirstOrDefault();
            return link;
        }
    }
}
