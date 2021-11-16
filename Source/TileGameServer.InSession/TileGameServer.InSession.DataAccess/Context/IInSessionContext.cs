using System.Collections.Generic;
using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.InSession.DataAccess.Context
{
    public interface IInSessionContext
    {
        ICollection<TEntity> EntitySet<TEntity>() where TEntity : BaseEntity;
    }
}