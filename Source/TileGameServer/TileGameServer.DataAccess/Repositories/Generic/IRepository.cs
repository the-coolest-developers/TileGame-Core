using System;
using System.Threading.Tasks;
using TileGameServer.Infrastructure.Models.Entities.Base;

namespace TileGameServer.DataAccess.Repositories.Generic
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        public Task CreateAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public Task<TEntity> GetAsync(Guid id);
        public Task DeleteAsync(Guid id);

        public Task SaveChangesAsync();

        public Task<bool> ExistsWithIdAsync(Guid id);
    }
}