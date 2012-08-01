using System;
using System.Data;
using Bruttissimo.Domain;

namespace Bruttissimo.Data.Dapper
{
	public class PictureRepository : IPictureRepository
	{
		private readonly IDbConnection connection;

		public PictureRepository(IDbConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.connection = connection;
		}
	}
}
