namespace Wheather.Services.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        void Add(TEntity entity);

        TEntity Get(TKey key);

        IEnumerable<TEntity> Get();

        void Delete(TKey key);

        void Update(TEntity entity);

        void Save();

        IRepository<TEntity, TKey> Include(string include);
    }
}