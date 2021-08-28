using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.Services
{
    public interface IQueryableUnitOfWork : IUnitOfWork, IDisposable, ISql
    {
        void ApplyCurrentValues<TEntity>(TEntity originalEntity, TEntity currentEntity) where TEntity : class;

        void Attach<TEntity>(TEntity item) where TEntity : class;

        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        void SetModified<TEntity>(TEntity item) where TEntity : class;
    }
}
