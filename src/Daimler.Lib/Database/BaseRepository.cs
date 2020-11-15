using Daimler.Lib.Domain;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Daimler.Lib.Database
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        private readonly IBaseContext<TEntity> _context;

        protected BaseRepository(IBaseContext<TEntity> context)
        {
            _context = context;
        }

        public virtual void Add(TEntity obj)
        {
            _context.Add(obj);
        }
 

        public virtual void Update(TEntity obj)
        {
            _context.Update(obj);
        }

        public virtual void Remove(int id)
        {
            _context.Remove(id);
        }

        public IEnumerable<TEntity> GetEntities()
        {
            return _context.GetEntities();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
