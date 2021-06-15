using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TileGameServer.Infrastructure.Models.Entities.Base;

namespace TileGameServer.DataAccess.Repositories.Generic
{
    public class EntityFrameworkBaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private DbContext EntityContext { get; }
        protected DbSet<TEntity> EntityDbSet => EntityContext.Set<TEntity>();

        //private DbSet<TEntity> Entities { get; }

        public EntityFrameworkBaseRepository(DbContext entityContext)
        {
            EntityContext = entityContext;
        }

        /*public EntityFrameworkBaseRepository(DbSet<TEntity> entityContext)
        {
            Entities = entityContext;
        }*/

        public async Task CreateAsync(TEntity entity)
        {
            await EntityDbSet.AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
            => Task.FromResult(EntityDbSet.Update(entity));

        public async Task<TEntity> GetAsync(Guid id)
            => await EntityDbSet.FindAsync(id);

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);

            EntityDbSet.Remove(entity);
            await EntityContext.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
            => EntityContext.SaveChangesAsync();

        public Task<bool> ExistsWithIdAsync(Guid id)
            => EntityDbSet.AnyAsync(entity => entity.Id == id);
    }
}