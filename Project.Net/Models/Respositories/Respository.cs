using Project.Net.Models.DbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Net.Models.Respositories
{
    public class Respository<TEntity> : IRespository<TEntity> where TEntity : class
    {
        protected ProjectDbContext _ctx;
        protected DbSet<TEntity> tbl;
        public Respository()
        {
            _ctx = new ProjectDbContext();
            tbl = _ctx.Set<TEntity>();
        }
        public bool Add(TEntity entity)
        {
            try
            {
                tbl.Add(entity);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public void Save()
        {
            _ctx.SaveChanges();
        }
        public bool Edit(TEntity entity)
        {
            try
            {
                _ctx.Entry(entity).State = EntityState.Modified;
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return tbl;
        }

        public TEntity Get(object id)
        {
            return tbl.Find(id);
        }
        public IEnumerable<TEntity> GetBy(Func<TEntity, bool> predicate)
        {
            return tbl.Where(predicate);
        }
        public bool Remove(object id)
        {
            try
            {
                tbl.Remove(Get(id));
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool Remove(TEntity entity)
        {
            try
            {
                tbl.Remove(entity);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                tbl.AddRange(entities);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                tbl.RemoveRange(entities);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public TEntity SingleBy(Func<TEntity, bool> predicate)
        {
            return tbl.FirstOrDefault(predicate);
        }

    }
}