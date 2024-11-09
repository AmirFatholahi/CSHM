using System.Data;
using Dapper;

namespace CSHM.Widget.Dapper;

public interface IDapperWidget
{
    List<T> CallProcedure<T>(string procedureName, DynamicParameters parameters, string serverName = "CSHMLogDB");

    Task<List<T>> CallProcedureAsync<T>(string procedureName, DynamicParameters parameters, string serverName = "CSHMLogDB");

    List<T> CallProcedure<T>(string procedureName, string connectionString, DynamicParameters parameters);

    Task<List<T>> CallProcedureAsync<T>(string procedureName, string connectionString, DynamicParameters parameters);

    DataTable CallProcedure(string procedureName, DynamicParameters parameters, string serverName = "CSHMLogDB");

    DataTable CallProcedure(string procedureName, string connectionString, DynamicParameters parameters);

    List<T> RunQuery<T>(string query, DynamicParameters parameters, string serverName = "CSHMLogDB");

    List<T> RunQuery<T>(string query, string connectionString, DynamicParameters parameters);

}