using System;
using System.Collections.Generic;

namespace TileGameServer.InSession.DataAccess.Context
{
    public class LazyInSessionContext : IInSessionContext
    {
        private readonly Dictionary<Type, ICollection<Type>> _entitySets;

        public LazyInSessionContext()
        {
            _entitySets = new Dictionary<Type, ICollection<Type>>();
        }

        public ICollection<TEntity> EntitySet<TEntity>()
        {
            var type = typeof(TEntity);
            if (_entitySets.ContainsKey(type))
            {
                var collection = new List<TEntity>();
                _entitySets.Add(type, (ICollection<Type>)collection);
            }

            var entitySet = (ICollection<TEntity>)_entitySets[type];
            return entitySet;
        }
    }
}
