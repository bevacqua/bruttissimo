using System;
using System.Data;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain;

namespace Bruttissimo.Data.Dapper
{
    public class PictureRepository : IPictureRepository
    {
        private readonly IDbConnection connection;

        public PictureRepository(IDbConnection connection)
        {
            Ensure.That(connection, "connection").IsNotNull();

            this.connection = connection;
        }
    }
}
