using Educational.Core.Repositories;
using Educational.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;

        }

        #region Async Functions

        public virtual async Task AddAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await _context.Set<TEntity>().AddRangeAsync(entities);
            }
            catch (Exception e)
            {
                var s = e.ToString();
                throw;
            }

        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            try
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();
                if (includes != null)
                {
                    query = includes(query);
                }

                if (condition != null)
                {
                    return await query.FirstOrDefaultAsync(condition);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
            {
                query = includes(query);
            }

            if (condition != null)
            {
                return await query.Where(condition).ToListAsync();
            }

            return await query.ToListAsync();
        }

        #endregion


        #region Sync Functions

        public void Add(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _context.Set<TEntity>().AddRange(entities);
            }
            catch (Exception e)
            {
                var s = e.ToString();
            }
        }


        public TEntity Get(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            try
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();

                if (includes != null)
                {
                    query = includes(query);
                }

                if (condition != null)
                {
                    return query.FirstOrDefault(condition);
                }

                return query.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }

            if (condition != null)
            {
                return query.Where(condition).ToList();
            }

            return query.ToList();
        }

        public void Remove(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
            }
            catch (Exception ex)
            {
                var s = ex.ToString();
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entites)
        {
            try
            {
                _context.Set<TEntity>().RemoveRange(entites);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        public TEntity GetById(Guid id)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            return _context.Set<TEntity>().FirstOrDefault();
        }
        public TEntity GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public virtual async Task<IEnumerable<TEntity>> ExecuteStoreQueryAsync(string commandText, params object[] parameters)
        {
            //  throw new NotImplementedException();
            try
            {
                return await _context.Set<TEntity>().FromSqlRaw(commandText, parameters).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public virtual async Task<IEnumerable<TEntity>> ExecuteStoreQueryAsync(string commandText, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            try
            {
                return await _context.Set<TEntity>().FromSqlRaw(commandText, includes).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
