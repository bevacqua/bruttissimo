using System;
using System.Data;
using Bruttissimo.Common.Guard;
using Bruttissimo.Domain;
using Dapper.Contrib.Extensions;

namespace Bruttissimo.Data.Dapper
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
