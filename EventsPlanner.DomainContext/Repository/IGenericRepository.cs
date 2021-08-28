using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.DomainContext.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }

        T GetSingle(Expression<Func<T, bool>> predicate);

        T GetSingle(Expression<Func<T, bool>> predicate, List<string> includes);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, List<string> includes);

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> GetAll(List<string> includes);

        Task<IEnumerable<T>> GetAllAsync(List<string> includes);

        IEnumerable<T> GetFiltered(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetFiltered(Expression<Func<T, bool>> predicate, List<string> includes);

        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate, List<string> includes);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Modify(T entity);

        //IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, Dictionary<string, object> parameters);

        //void ExecuteQuery(string sqlQuery, Dictionary<string, object> parameters);

        //void ExecuteQuery(SqlParameter[] parameters, string sqlQuery);

        //IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string storeProcedure, Dictionary<string, object> parameters);

        //IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string storeProcedure, SqlParameter[] parameters);

        //void ApplyCurrentValues<TEntity>(TEntity originalEntity, TEntity currentEntity) where TEntity : class;

        //void Attach<TEntity>(TEntity item) where TEntity : class;

        //DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        //void SetModified<TEntity>(TEntity item) where TEntity : class;

        //int ExecuteCommand(string sqlCommand, params object[] parameters);

        //Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters);

        //IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlCommand, params object[] parameters);

        //Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string sqlCommand, params object[] parameters);

        //TType ExecuteScalarFunction<TType>(string scalarFunction, params object[] parameters);

        //Task<TType> ExecuteScalarFunctionAsync<TType>(string scalarFunction, params object[] parameters);

    }
}
