using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackEnd.OpheliaTest.Entities.Interface.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        DbContext AppContext { get; set; }

        DbSet<T> Entity => AppContext.Set<T>();

        Task<int> AddAsync(List<T> values);

        Task<int> AddAsync(T value);

        Task<int> DeleteAsync(T value = null, Expression<Func<T, bool>> predicate = null);

        Task<int> DeleteAsync(List<T> values = null);

        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, T>> selector = null);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Expression<Func<T, T>> selector = null);

        Task<int> UpdateAsync(List<T> values, params Expression<Func<T, object>>[] propertyExpressions);

        Task<int> UpdateAsync(T value, params Expression<Func<T, object>>[] propertyExpressions);
    }
}