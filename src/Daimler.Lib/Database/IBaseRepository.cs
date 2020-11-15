using System;
using System.Collections.Generic;
using Daimler.Lib.Domain;

namespace Daimler.Lib.Database
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : IEntity
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(int id);
        IEnumerable<TEntity> GetEntities();
    }
}
