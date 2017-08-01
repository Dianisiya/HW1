namespace Wheather.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        Task Add(TEntity entity);

        Task<TEntity> Get(TKey key);

        Task<IEnumerable<TEntity>> Get();

        Task Delete(TKey key);

        Task Update(TEntity entity);

        Task Save();

        IRepository<TEntity, TKey> Include(string include);
    }
}