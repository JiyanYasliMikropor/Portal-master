using Microsoft.EntityFrameworkCore;
using Mikroportal.MODEL.RequestResponseClasses.ACS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Mikroportal.DATA
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        public DbSet<TEntity> dbSet;
        internal GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {

                return query;
            }
        }
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual bool Insert(TEntity entity)
        {
            var query = dbSet.Add(entity);

            if (query != null)
                return true;
            return false;
        }

        //public int InsertReturnId(TEntity entity)
        //{
        //    var query = dbSet.Add(entity);

        //    return entity.;
        //}

        public virtual bool Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            return Delete(entityToDelete);
        }
        public virtual bool Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            var query = dbSet.Remove(entityToDelete);

            if (query != null)
                return true;
            return false;
        }
        public virtual bool Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            var query = context.Entry(entityToUpdate).State = EntityState.Modified;

            if (query == EntityState.Modified)
                return true;
            return false;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable<TEntity>().ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }

    
    }
}
