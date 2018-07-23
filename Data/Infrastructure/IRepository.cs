using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Edit(T entity);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);

        T GetById(int? id);

        //IQueryable<T> GetAll();
        IEnumerable<T> GetAll();
        DbSet<T> GetTable();
    }
}
