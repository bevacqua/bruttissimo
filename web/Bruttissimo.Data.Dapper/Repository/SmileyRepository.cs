using System.Data;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain.Entity.Entities;
using Bruttissimo.Domain.Repository;

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
    }
}
