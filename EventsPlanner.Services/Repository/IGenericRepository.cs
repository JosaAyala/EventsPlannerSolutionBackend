using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.Services.Repository
{
    public interface IGenericRepository<T> where T : IQueryableUnitOfWork
    {
        IUnitOfWork UnitOfWork { get; }

        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : class;

        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) 
            where TEntity : class;

        Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes)
            where TEntity : class;

        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
            where TEntity : class;

        IEnumerable<TEntity> GetAll<TEntity>(List<string> includes) 
            where TEntity : class;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(List<string> includes) 
            where TEntity : class;

        IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : class;

        IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) 
            where TEntity : class;

        Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) 
            where TEntity : class;

        Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes)
            where TEntity : class;
        void Add<TEntity>(TEntity entity)
            where TEntity : class;

        void AddRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;

        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Modify<TEntity>(TEntity entity) where TEntity : class;

        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, Dictionary<string, object> parameters);

        void ExecuteQuery(string sqlQuery, Dictionary<string, object> parameters);

        void ExecuteQuery(SqlParameter[] parameters, string sqlQuery);

        IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string storeProcedure, Dictionary<string, object> parameters);

        IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string storeProcedure, SqlParameter[] parameters);

    }
}
