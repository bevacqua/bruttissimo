using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Dapper;

namespace Bruttissimo.Data.Dapper.Repository
{
    public class LinkRepository : EntityRepository<Link>, ILinkRepository
    {
        private readonly IDbConnection connection;

        public LinkRepository(IDbConnection connection)
            : base(connection)
        {
            Ensure.That(connection, "connection").IsNotNull();

            this.connection = connection;
        }

	    public Link GetByReferenceUri(Uri referenceUri)
	    {
		    Link link = GetByReferenceUri(referenceUri.ToString());
		    return link;
	    }

		public Link GetByReferenceUri(string referenceUri)
		{
			const string sql = @"
				SELECT [Link].*, [Post].[Id] AS [PostId]
				FROM [Link]
				LEFT JOIN [Post] ON [Post].[LinkId] = [Link].[Id]
				WHERE [Link].[ReferenceUri] = @referenceUri
            ";
			IEnumerable<Link> result = connection.Query<Link>(sql, new { referenceUri  });

			Link link = result.FirstOrDefault();
			return link;
		}
    }
}
