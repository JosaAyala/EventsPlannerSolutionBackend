using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsPlanner.Services
{
    public interface ISql
    {
        int ExecuteCommand(string sqlCommand, params object[] parameters);

        Task<int> ExecuteCommandAsync(string sqlCommand, params object[] parameters);

        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlCommand, params object[] parameters);

        Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string sqlCommand, params object[] parameters);

        TType ExecuteScalarFunction<TType>(string scalarFunction, params object[] parameters);

        Task<TType> ExecuteScalarFunctionAsync<TType>(string scalarFunction, params object[] parameters);
    }
}