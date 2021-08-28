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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DbSet<T> GetSet()
        {
            return _unitOfWork.Context.Set<T>();
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

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return GetSet().FirstOrDefault(predicate);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, List<string> includes)
        {
            IQueryable<T> items = GetSet();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return items.FirstOrDefault(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetSet().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, List<string> includes)
        {
            IQueryable<T> items = GetSet();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return await items.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return GetSet().ToList();
        }

        public IEnumerable<T> GetAll(List<string> includes)
        {
            IQueryable<T> items = GetSet();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return items.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetSet().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(List<string> includes)
        {
            IQueryable<T> items = GetSet();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return await items.ToListAsync();
        }

        public IEnumerable<T> GetFiltered(Expression<Func<T, bool>> predicate)
        {
            return GetSet().Where(predicate).ToList();
        }

        public IEnumerable<T> GetFiltered(Expression<Func<T, bool>> predicate, List<string> includes)
        {
            IQueryable<T> items = GetSet();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return items.Where(predicate).ToList();
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetSet().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate, List<string> includes)
        {
            IQueryable<T> items = GetSet();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return await items.Where(predicate).ToListAsync();
        }

        public void Add(T entity)
        {
            GetSet().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            GetSet().AddRange(entities);
        }

        public void Remove(T entity)
        {
            //attach item if not exist
            GetSet().Attach(entity);

            //set as "removed"
            GetSet().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            GetSet().RemoveRange(entities);
        }

        public void Modify(T entity)
        {
            GetSet().Update(entity);
        }


        //private SqlParameter[] CreateSqlParameters(Dictionary<string, object> parameters)
        //{
        //    if (parameters != null && parameters.Any())
        //    {
        //        return (from qry in parameters select new SqlParameter(qry.Key, qry.Value)).ToArray();
        //    }

        //    return new SqlParameter[0];
        //}

        //public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, Dictionary<string, object> parameters)
        //{
        //    SqlParameter[] sqlParameters = CreateSqlParameters(parameters);
        //    return _unitOfWork.Context.ExecuteQuery<TEntity>(sqlQuery, sqlParameters).ToList();
        //}

        //public void ExecuteQuery(string sqlQuery, Dictionary<string, object> parameters)
        //{
        //    SqlParameter[] sqlParameters = CreateSqlParameters(parameters);
        //    _unitOfWork.ExecuteCommand(sqlQuery, sqlParameters);
        //}

        //public void ExecuteQuery(SqlParameter[] parms, string sqlQuery)
        //{
        //    _unitOfWork.ExecuteCommand(sqlQuery, parms);
        //}

        //public TType ExecuteScalarFunction<TType>(string scalarFunction, Dictionary<string, object> parameters)
        //{
        //    SqlParameter[] sqlParameters = CreateSqlParameters(parameters);
        //    string paramNames = GetParamNames(parameters);

        //    var result = (string.IsNullOrWhiteSpace(paramNames))
        //        ? _unitOfWork.ExecuteScalarFunction<TType>(string.Format("SELECT {0}();", scalarFunction), sqlParameters)
        //        : _unitOfWork.ExecuteScalarFunction<TType>(string.Format("SELECT {0}({1});", scalarFunction, paramNames), sqlParameters);

        //    return result;
        //}

        //public IEnumerable<TType> ExecuteStoredProcedure<TType>(string storedProcedure, Dictionary<string, object> parameters)
        //{
        //    SqlParameter[] sqlParameters = CreateSqlParameters(parameters);
        //    string paramNames = GetParamNames(parameters);

        //    return (string.IsNullOrWhiteSpace(paramNames))
        //        ? _unitOfWork.Context.ExecuteQuery<TType>(string.Format("EXEC {0}", storedProcedure), sqlParameters).ToList()
        //        : _unitOfWork.ExecuteQuery<TType>(string.Format("EXEC {0} {1}", storedProcedure, paramNames), sqlParameters).ToList();
        //}

        //public IEnumerable<TType> ExecuteStoredProcedure<TType>(string storedProcedure, SqlParameter[] parameters)
        //{
        //    string paramNames = GetParamNames(parameters);
        //    return _unitOfWork.ExecuteQuery<TType>(string.Format("EXEC {0} {1}", storedProcedure, paramNames), parameters).ToList();
        //}
    }
}
