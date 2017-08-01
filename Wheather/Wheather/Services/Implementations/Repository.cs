namespace Wheather.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using Wheather.Models.Db;
    using Wheather.Services.Interfaces;

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseDbEntity<TKey>
    {
        private readonly WeatherDb context;

        private readonly DbSet<TEntity> dbSet; 

        public Repository(WeatherDb context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
            this.includes = new string[0];
        }

        private Repository(WeatherDb context, string[] includes)
            :this(context)
        {
            this.includes = includes;
        }

        public async Task Add(TEntity entity)
        {
            this.dbSet.Add(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task<TEntity> Get(TKey key)
        {
            return await this.dbSet.FindAsync(key);
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            var entities = this.dbSet.AsQueryable();
            if(this.includes != null)
            foreach (var include in this.includes)
            {
                entities = entities.Include(include);
            }
            return await entities.ToArrayAsync();
        }

        public async Task Delete(TKey key)
        {
           this.dbSet.Remove(await this.Get(key));
        }

        public async Task Update(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
        }

        public async Task Save()
        {
            await this.context.SaveChangesAsync();
        }

        private readonly string[] includes;

        public IRepository<TEntity, TKey> Include(string include)
        {
            if (string.IsNullOrEmpty(include))
            {
                throw new ArgumentException();
            }
            return new Repository<TEntity, TKey>(this.context, this.includes.Union(new [] {include}).ToArray());
        }
    }
}