using System;
using System.Collections;
using System.Collections.Generic;
using WebApiBaseLibrary.DataAccess.Entities;

namespace TileGameServer.InSession.DataAccess.Context
{
    public class LazyInSessionContext : IInSessionContext
    {
        private readonly Dictionary<Type, ICollection> _entitySets = new();

        public ICollection<TEntity> EntitySet<TEntity>()
            where TEntity : BaseEntity
        {
            var type = typeof(TEntity);
            if (!_entitySets.ContainsKey(type))
            {
                ICollection<TEntity> collection = new List<TEntity>();
                _entitySets.Add(type, (ICollection)collection);
            }

            var entitySet = (ICollection<TEntity>)_entitySets[type];
            return entitySet;
        }
    }
}