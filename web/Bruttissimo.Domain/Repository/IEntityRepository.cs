namespace Bruttissimo.Domain
{
    public interface IEntityRepository<T>
    {
        T GetById(long id);
        T Insert(T entity);
        T Update(T entity);
        bool Delete(T entity);
    }
}