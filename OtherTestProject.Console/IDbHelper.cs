using System.Data;

namespace OtherTestProject.Console
{
    /// <summary>
    /// 接口定义
    /// </summary>
    public interface IDbHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="commandParameters"></param>
        void CacheParameters(string cacheKey, params IDataParameter[] commandParameters);
        int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);
        int ExecuteNonQuery(IDbTransaction trans, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);
        int ExecuteNonQuery(IDbConnection connection, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);

        IDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);

        object ExecuteScalar(IDbTransaction trans, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);
        object ExecuteScalar(IDbConnection connection, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);
        object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);

        DataSet ExecuteDataset(IDbTransaction trans, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);
        DataSet ExecuteDataset(IDbConnection connection, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);
        DataSet ExecuteDataset(string connectionString, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters);
        DataSet ExecuteDataset(IDbConnection connection, CommandType cmdType, string cmdText, string CacheName, params IDataParameter[] commandParameters);

        IDataParameter[] GetCachedParameters(string cacheKey);

        IDataParameter CreateInParameter(string parameterName, object parameterValue);
        IDataParameter CreateInParameter(string parameterName, DbType dbType, int dbSize);
        IDataParameter CreateOutParameter(string parameterName, DbType dbType, int dbSzie);
        IDataParameter CreateInOutParameter(string parameterName, DbType dbType, int dbSzie);

        IDbConnection GetConnection(string connectionString);
        
        int DoubleExecuteNonQuery(IDbTransaction trans, string connectionStringB, CommandType cmdType, string sentence, params IDataParameter[] parameters);
        int DoubleExecuteNonQuery(string connectionStringA, string connectionStringB, CommandType cmdType, string sentence, params IDataParameter[] parameters);
        int DoubleExecuteNonQuery(string connectionStringA, string connectionStringB, CommandType cmdType, string sentence);
        int ExecutBathInsert(System.Data.SqlClient.SqlCommand insertcommand, IDbConnection connection, DataSet ds, string tablename);
    }
}
