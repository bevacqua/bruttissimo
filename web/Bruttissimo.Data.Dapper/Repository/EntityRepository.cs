using System;
using System.Data;
using Bruttissimo.Domain;
using Dapper.Contrib.Extensions;

namespace Bruttissimo.Data.Dapper
{
    public abstract class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly IDbConnection connection;

        protected EntityRepository(IDbConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            this.connection = connection;
        }

        public virtual T GetById(long id)
        {
            T entity = connection.Get<T>(id);
            return entity;
        }

        public virtual T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            connection.Insert(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            connection.Update(entity);
            return entity;
        }

        public virtual bool Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            bool result = connection.Delete(entity);
            return result;
        }
    }
}
