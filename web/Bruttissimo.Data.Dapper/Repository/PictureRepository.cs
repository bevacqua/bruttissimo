using System.Data;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Repository;

namespace Bruttissimo.Data.Dapper.Repository
{
    public class PictureRepository : IPictureRepository
    {
        private readonly IDbConnection connection;

        public PictureRepository(IDbConnection connection)
        {
            Ensure.That(() => connection).IsNotNull();

            this.connection = connection;
        }
    }
}
