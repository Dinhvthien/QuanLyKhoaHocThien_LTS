﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Domain.IRespositories
{
    public interface IBaseRespository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(int id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<int> CountAsync(string inclucde, Expression<Func<TEntity, bool>> expression = null);
    }
}
