using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);
        void Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);


        TEntity GetById(Guid id);


        Task<IEnumerable<TEntity>> ExecuteStoreQueryAsync(String commandText, params object[] parameters);
        Task<IEnumerable<TEntity>> ExecuteStoreQueryAsync(String commandText, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> includes = null);

        #region     

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        TEntity Get(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entites);

        IQueryable<TEntity> GetAll();
        #endregion
    }
}
