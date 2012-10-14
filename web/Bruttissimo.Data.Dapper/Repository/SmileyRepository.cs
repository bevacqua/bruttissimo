using System.Collections.Generic;
using System.Data;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;
using Dapper;

namespace Bruttissimo.Data.Dapper.Repository
{
    public class SmileyRepository : EntityRepository<Smiley>, ISmileyRepository
    {
        private readonly IDbConnection connection;

        public SmileyRepository(IDbConnection connection)
            : base(connection)
        {
            Ensure.That(() => connection).IsNotNull();

            this.connection = connection;
        }

        public IEnumerable<Smiley> GetSmileys()
        {
            const string sql = @"SELECT [Smiley].* FROM [Smiley]";
            IEnumerable<Smiley> smileys = connection.Query<Smiley>(sql);
            return smileys;
        }
    }
}
