using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.ImplementRespositories
{
    public class BaseRespository<TEntity> : IBaseRespository<TEntity> where TEntity : class
    {

        protected IDBContext _idbContext = null;
        protected DbSet<TEntity> _dbSet;
        protected DbContext _dbContext;
        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = _dbContext.Set<TEntity>() as DbSet<TEntity>;
                }
                return _dbSet;
            }
        }

        public BaseRespository(IDBContext dbContext)
        {
            _idbContext = dbContext;
            _dbContext = (DbContext)dbContext;
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = expression != null ? DbSet.Where(expression): DbSet;
            return await query.CountAsync();
        }

        public async Task<int> CountAsync(string inclucde, Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query;
            if (!string.IsNullOrEmpty(inclucde))
            {
                query = BuildQueryable(new List<string>() { inclucde }, expression);
                return await query.CountAsync();
            }
            return await CountAsync(expression); 
        }
        protected IQueryable<TEntity> BuildQueryable(List<string> includes, Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = DbSet.AsQueryable();
            if (includes != null && includes.Count > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include.Trim());
                }
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return query;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await _idbContext.CommitchangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
            await _idbContext.CommitchangesAsync();
            return entities;
        }
         
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
           IQueryable<TEntity> query = expression!= null ? DbSet.Where(expression) : DbSet;
            var dataEntity = query;
            if (dataEntity != null)
            {
                DbSet.RemoveRange(dataEntity);
                await _idbContext.CommitchangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var dataEntity = await DbSet.FindAsync(id);
            if (dataEntity!= null)
            {
                DbSet.Remove(dataEntity);
                await _idbContext.CommitchangesAsync();
            }
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = expression != null ? DbSet.Where(expression) : DbSet;
            return query;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression = null)
        {
          
            return await DbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _idbContext.CommitchangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities) {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _idbContext.CommitchangesAsync();
            return entities;
        }
    }
}
