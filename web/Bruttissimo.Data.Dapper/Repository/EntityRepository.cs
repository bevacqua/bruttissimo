using System.Data;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain;
using Bruttissimo.Domain.Repository;

namespace Bruttissimo.Data.Dapper.Repository
{
    public abstract class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly IDbConnection connection;

        protected EntityRepository(IDbConnection connection)
        {
            Ensure.That(connection, "connection").IsNotNull();

            this.connection = connection;
        }

        public virtual T GetById(long id)
        {
            T entity = connection.Get<T>(id);
            return entity;
        }

        public virtual T Insert(T entity)
        {
            Ensure.That(entity, "entity").IsNotNull();

            connection.Insert(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            Ensure.That(entity, "entity").IsNotNull();

            connection.Update(entity);
            return entity;
        }

        public virtual bool Delete(T entity)
        {
            Ensure.That(entity, "entity").IsNotNull();

            bool result = connection.Delete(entity);
            return result;
        }
    }
}
