using BackEnd.OpheliaTest.Entities.Interface.Repository;
using BackEnd.OpheliaTest.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace BackEnd.OpheliaTest.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public BaseRepository(OPHELIATESTContext context)
        {
            AppContext = context;
        }

        public DbContext AppContext { get; set; }

       
        public DbSet<T> Entity => AppContext.Set<T>();

       
        public async Task<int> AddAsync(T value)
        {
            await Entity.AddAsync(value);

            return await AppContext.SaveChangesAsync();
        }

       
        public async Task<int> AddAsync(List<T> values)
        {
            await Entity.AddRangeAsync(values);

            return await AppContext.SaveChangesAsync();
        }

      
        public async Task<int> UpdateAsync(T value, params Expression<Func<T, object>>[] propertyExpressions)
        {
            if (propertyExpressions == null || propertyExpressions.Length <= 0)
            {
                var entity = Entity.Update(value);
                entity.State = EntityState.Modified;
            }
            else
            {
                if (Entity.Local.Any())
                {
                    foreach (var entity in Entity.Local)
                    {
                        AppContext.Entry(entity).State = EntityState.Detached;
                    }
                }
                Entity.Attach(value);
                foreach (var column in propertyExpressions)
                {
                    AppContext.Entry(value).Property(column).IsModified = true;
                }
            }

            return await AppContext.SaveChangesAsync();
        }

       
        public async Task<int> UpdateAsync(List<T> values, params Expression<Func<T, object>>[] propertyExpressions)
        {
            foreach (var value in values)
            {
                await UpdateAsync(value, propertyExpressions);
            }
            return await Task.Run(() => values.Count);
        }

      
        public async Task<int> DeleteAsync(T value = null, Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                AppContext.Entry(value).State = EntityState.Deleted;
                AppContext.Remove(value);
            }
            else
            {
                IEnumerable<T> entities = Entity.Where(predicate);

                foreach (var entity in entities)
                {
                    AppContext.Entry(entity).State = EntityState.Deleted;
                    AppContext.Remove(entity);
                }
            }

            return await AppContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(List<T> values = null)
        {
            foreach (var entity in values)
            {
                AppContext.Entry(entity).State = EntityState.Deleted;
                AppContext.Remove(entity);
            }

            return await AppContext.SaveChangesAsync();
        }

        
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                      Expression<Func<T, T>> selector = null)
        {
            IQueryable<T> query = Entity.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                if (selector != null)
                {
                    return await orderBy(query).Select(selector).AsNoTracking().FirstOrDefaultAsync();
                }

                return await orderBy(query).AsNoTracking().FirstOrDefaultAsync();
            }

            if (selector != null)
            {
                return await query.Select(selector).AsNoTracking().FirstOrDefaultAsync();
            }

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

      
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
                                                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                Expression<Func<T, T>> selector = null)
        {
            IQueryable<T> query = Entity.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                if (selector != null)
                {
                    return await orderBy(query).Select(selector).AsNoTracking().ToListAsync();
                }

                return await orderBy(query).AsNoTracking().ToListAsync();
            }

            if (selector != null)
            {
                return await query.Select(selector).AsNoTracking().ToListAsync();
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}