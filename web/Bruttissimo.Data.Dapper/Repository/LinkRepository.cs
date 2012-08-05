using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Entity;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Bruttissimo.Data.Dapper
{
	public class LinkRepository : ILinkRepository
	{
		private readonly IDbConnection connection;

		public LinkRepository(IDbConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.connection = connection;
		}
		
		public Link GetById(long id)
		{
			Link link = connection.Get<Link>(id);
			return link;
		}

		public Link GetByReferenceUri(Uri referenceUri)
		{
			if (referenceUri == null)
			{
				throw new ArgumentNullException("referenceUri");
			}
			IEnumerable<Link> result = connection.Query<Link>(@"
				SELECT [Link].*, [Post].[Id] AS [PostId]
				FROM [Link]
				LEFT JOIN [Post] ON [Post].[LinkId] = [Link].[Id]
				WHERE [Link].[ReferenceUri] = @referenceUri", new { referenceUri = referenceUri.AbsoluteUri });

			Link link = result.FirstOrDefault();
			return link;
		}

		public Link Insert(Link link)
		{
			if (link == null)
			{
				throw new ArgumentNullException("link");
			}
			connection.Insert(link);
			return link;
		}
	}
}
