using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts
{
    public interface IDBContext : IDisposable
    {
        DbSet<TEntity> SetEntity<TEntity>() where TEntity : class; // <==>
        Task<int> CommitchangesAsync();
    }
}
