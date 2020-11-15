using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Daimler.Lib.Domain;
using Daimler.Lib.Helpers;
using Daimler.Lib.Logger;
using Microsoft.AspNetCore.Http;

namespace Daimler.Lib.Database
{
    public interface IBaseContext<TEntity> : IDisposable where TEntity : IEntity
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(int id);
        IEnumerable<TEntity> GetEntities();
    }

    // db icin inmemory bir context olusturdum, basit veri yonetimi icin
    public class BaseContext<TEntity> : IBaseContext<TEntity> where TEntity : Entity
    {
        // multi-thread calismasindaki olusacak problemler gozardi edildi. 
        private readonly ConcurrentBag<TEntity> _entities = new ConcurrentBag<TEntity>();
        private int _id;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDaimlerLogger _daimlerLogger;


        public BaseContext(IHttpContextAccessor httpContextAccessor, IDaimlerLogger daimlerLogger)
        {
            _httpContextAccessor = httpContextAccessor;
            _daimlerLogger = daimlerLogger;

        }

        public void Add(TEntity obj)
        {
            Interlocked.Increment(ref _id);
            obj.Id = _id;
            obj.CreatedBy = _httpContextAccessor.HttpContext.GetCurrentUsername();
            obj.CreatedDate = DateTime.Now;
            var item = _entities.SingleOrDefault(x => x.Id == obj.Id && (x.ValidFor == null || x.ValidFor >= DateTime.Now));
            if (item != null) throw new Exception("Bu kayit daha once olusturulmus.");
            _entities.Add(obj);
            _daimlerLogger.Information("Kayit Eklendi.");
        }

        public void Update(TEntity obj)
        {
            var item = _entities.SingleOrDefault(x => x.Id == obj.Id && (x.ValidFor == null || x.ValidFor >= DateTime.Now));
            if (item != null)
            {
                obj.CreatedBy = item.CreatedBy;
                obj.CreatedDate = item.CreatedDate;
                obj.UpdatedBy = _httpContextAccessor.HttpContext.GetCurrentUsername();
                obj.UpdatedDate = DateTime.Now;
                _entities.TryTake(out item);
                _entities.Add(item);
                _daimlerLogger.Information("Kayit Guncellendi.");
            }
            else
            {
                throw new Exception("Guncellenecek kayit bulunamadi.");
            }
        }

        public void Remove(int id)
        {
            var item = _entities.SingleOrDefault(x => x.Id == id && (x.ValidFor == null || x.ValidFor >= DateTime.Now));
            if (item != null)
            {
                item.ValidFor = DateTime.Now;
                item.UpdatedBy = _httpContextAccessor.HttpContext.GetCurrentUsername();
                item.UpdatedDate = DateTime.Now;
                _daimlerLogger.Information("Kayit Silindi.");
            }
            else
            {
                throw new Exception("Silinecek kayit bulunamadi.");
            }
        }

        public IEnumerable<TEntity> GetEntities()
        {
            return _entities;
        }

        public void Dispose()
        {
        }
    }
}