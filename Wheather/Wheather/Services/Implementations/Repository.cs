namespace Wheather.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

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

        public void Add(TEntity entity)
        {
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        public TEntity Get(TKey key)
        {
            return this.dbSet.Find(key);
        }

        public IEnumerable<TEntity> Get()
        {
            var entities = this.dbSet.AsQueryable();
            if(this.includes != null)
            foreach (var include in this.includes)
            {
                entities = entities.Include(include);
            }
            return entities.ToArray();
        }

        public void Delete(TKey key)
        {
            this.dbSet.Remove(this.Get(key));
        }

        public void Update(TEntity entity)
        {
            this.dbSet.Remove(this.Get(entity.Id));
            this.dbSet.Add(entity);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        private readonly string[] includes;

        public IRepository<TEntity, TKey> Include(string include)
        {
            return new Repository<TEntity, TKey>(this.context, this.includes.Union(new [] {include}).ToArray());
        }
    }
}