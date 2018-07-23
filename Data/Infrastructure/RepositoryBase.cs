using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private ForumEntities dataContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ForumEntities DbContext
        {
            get
            {
                return dataContext ?? (dataContext = DbFactory.Init());
            }
        }

        #endregion
        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }
        #region Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Edit(T entity)
        {
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IQueryable<T> objects = dbSet.Where<T>(where).AsQueryable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(int? id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
            //return dbSet.AsQueryable();
        }


        public virtual DbSet<T> GetTable()
        {
            return DbContext.Set<T>();
        }
        #endregion
    }
}
