using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : IQueryableUnitOfWork
    {
        private readonly T _unitOfWork;

        public GenericRepository(T unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private DbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return _unitOfWork.CreateSet<TEntity>();
        }

        private string GetParamNames(SqlParameter[] parameters)
        {
            return (parameters != null && parameters.Any()) ?
                parameters.Select(p => p.ParameterName).Aggregate((i, j) => i + "," + j) 
                : string.Empty;
        }

        private string GetParamNames(Dictionary<string, object> parameters)
        {
            return (parameters != null && parameters.Any())
                ? parameters.Select(p => p.Key).Aggregate((i, j) => i + ", " + j)
                : string.Empty;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetSet<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : class
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return items.FirstOrDefault(predicate);
        }

        public async Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await GetSet<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : class
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return await items.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetSet<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAll<TEntity>(List<string> includes) where TEntity : class
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return items.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            return await GetSet<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(List<string> includes) where TEntity : class
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return await items.ToListAsync();
        }

        public IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetSet<TEntity>().Where(predicate).ToList();
        }

        public IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : class
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return items.Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await GetSet<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : class
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return await items.Where(predicate).ToListAsync();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            GetSet<TEntity>().Add(entity);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            GetSet<TEntity>().AddRange(entities);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            //attach item if not exist
            GetSet<TEntity>().Attach(entity);

            //set as "removed"
            GetSet<TEntity>().Remove(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            GetSet<TEntity>().RemoveRange(entities);
        }

        public void Modify<TEntity>(TEntity entity) where TEntity : class
        {
            _unitOfWork.SetModified(entity);
        }

        private SqlParameter[] CreateSqlParameters(Dictionary<string, object> parameters)
        {
            if (parameters != null && parameters.Any())
            {
                return (from qry in parameters select new SqlParameter(qry.Key, qry.Value)).ToArray();
            }

            return new SqlParameter[0];
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, Dictionary<string, object> parameters)
        {
            SqlParameter[] sqlParameters = CreateSqlParameters(parameters);
            return _unitOfWork.ExecuteQuery<TEntity>(sqlQuery, sqlParameters).ToList();
        }

        public void ExecuteQuery(string sqlQuery, Dictionary<string, object> parameters)
        {
            SqlParameter[] sqlParameters = CreateSqlParameters(parameters);
            _unitOfWork.ExecuteCommand(sqlQuery, sqlParameters);
        }

        public void ExecuteQuery(SqlParameter[] parms, string sqlQuery)
        {
            _unitOfWork.ExecuteCommand(sqlQuery, parms);
        }

        public TType ExecuteScalarFunction<TType>(string scalarFunction, Dictionary<string, object> parameters)
        {
            SqlParameter[] sqlParameters = CreateSqlParameters(parameters);
            string paramNames = GetParamNames(parameters);

            var result = (string.IsNullOrWhiteSpace(paramNames))
                ? _unitOfWork.ExecuteScalarFunction<TType>(string.Format("SELECT {0}();", scalarFunction), sqlParameters)
                : _unitOfWork.ExecuteScalarFunction<TType>(string.Format("SELECT {0}({1});", scalarFunction, paramNames), sqlParameters);

            return result;
        }

        public IEnumerable<TType> ExecuteStoredProcedure<TType>(string storedProcedure, Dictionary<string, object> parameters)
        {
            SqlParameter[] sqlParameters = CreateSqlParameters(parameters);
            string paramNames = GetParamNames(parameters);

            return (string.IsNullOrWhiteSpace(paramNames))
                ? _unitOfWork.ExecuteQuery<TType>(string.Format("EXEC {0}", storedProcedure), sqlParameters).ToList()
                : _unitOfWork.ExecuteQuery<TType>(string.Format("EXEC {0} {1}", storedProcedure, paramNames), sqlParameters).ToList();
        }

        public IEnumerable<TType> ExecuteStoredProcedure<TType>(string storedProcedure, SqlParameter[] parameters)
        {
            string paramNames = GetParamNames(parameters);
            return _unitOfWork.ExecuteQuery<TType>(string.Format("EXEC {0} {1}", storedProcedure, paramNames), parameters).ToList();
        }
    }
}
