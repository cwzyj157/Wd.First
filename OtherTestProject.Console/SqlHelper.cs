using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace OtherTestProject.Console
{
    /// <summary>
    /// IDbHelper基础类
    /// </summary>
    public class SqlHelper : IDbHelper  
    {
        #region IDbHelper 成员
        #region 哈希表用来存储缓存的参数信息，哈希表可以存储任意类型的参数。
        /// <summary>
        /// 哈希表用来存储缓存的参数信息，哈希表可以存储任意类型的参数。
        /// </summary>
        private Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        #endregion
        
        #region 数据库同步执行ExecuteNonQuery方法
        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //清空SqlCommand中的参数列表
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程
        /// </summary>
        /// <param name="trans">数据库访问事务</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(IDbTransaction trans, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(IDbConnection connection, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }        
        #endregion

        #region ExecuteReader方法
        /// <summary>
        /// 获取数据库读取器
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>读取器</returns>
        public IDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // 在这里使用try/catch处理是因为如果方法出现异常，则SqlDataReader就不存在，
            //CommandBehavior.CloseConnection的语句就不会执行，触发的异常由catch捕获。
            //关闭数据库连接，并通过throw再次引发捕捉到的异常。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }
        }
        #endregion

        #region ExecuteScalar方法
        /// <summary>
        /// 获取数据集的第一行第一列
        /// </summary>
        /// <param name="trans">数据库访问事务</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>数据集<</returns>
        public object ExecuteScalar(IDbTransaction trans, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 获取数据集的第一行第一列
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>数据集<</returns>
        public object ExecuteScalar(IDbConnection connection, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 获取数据集的第一行第一列
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>数据集</returns>
        public object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        #endregion

        #region 获取数据集
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="trans">数据库访问事务</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataset(IDbTransaction trans, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataset(IDbConnection connection, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public int ExecutBathInsert(System.Data.SqlClient.SqlCommand insertcommand, IDbConnection connection, DataSet ds, string tablename)
        {
            using (SqlDataAdapter dsinsert = new SqlDataAdapter())
            {
                insertcommand.Connection = (SqlConnection)connection;
                dsinsert.InsertCommand = insertcommand;
                int ireturn = dsinsert.Update(ds, tablename);
                ds.Tables[tablename].AcceptChanges();
                return ireturn;
            }

        }

        /// <summary>
        /// 填充DataSet,增加了缓存依赖
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="cmdType">Sql类型</param>
        /// <param name="cmdText">Sql命令</param>
        /// <param name="CacheName">缓存名称</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>结果集</returns>
        /// <remarks>
        /// add by wcz 2010-5-28
        /// </remarks>
        public DataSet ExecuteDataset(IDbConnection connection, CommandType cmdType, string cmdText, string CacheName, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                SqlDependency sqldep = new SqlDependency(da.SelectCommand, null, 0);
                da.Fill(ds);
                sqldep.OnChange += new OnChangeEventHandler(sqldep_OnChange);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sqldep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdType">SQL字符串类型</param>
        /// <param name="cmdText">SQL字符串</param>
        /// <param name="commandParameters">SQL参数列表</param>
        /// <returns>数据集</returns>
        public DataSet ExecuteDataset(string connectionString, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    string e = ex.Message;
                }
                cmd.Parameters.Clear();
                return ds;
            }
        }        
        #endregion

        #region 数据库访问基类其他方法
        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">SqlCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-SQL语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private void PrepareCommand(IDbCommand cmd, IDbConnection conn, IDbTransaction trans, CommandType cmdType, string cmdText, IDataParameter[] cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //判断是否需要事物处理
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    //modify by limx 20100719 传null的parm                   
                    if (parm != null)
                        cmd.Parameters.Add(parm);
                }
            }
        }

        /// <summary>
        /// 缓存参数
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="commandParameters"></param>
        public void CacheParameters(string cacheKey, params IDataParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 得到数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>连接对象</returns>
        public IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

       

        /// <summary>
        /// 使用克隆方法创建参数列表
        /// </summary>
        /// <param name="cacheKey">缓存key</param>
        /// <returns>克隆的SQL参数列表</returns>
        public IDataParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
            {
                return null;
            }
            //新建一个参数的克隆列表
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
            //通过循环为克隆参数列表赋值
            for (int i = 0, j = cachedParms.Length; i < j; i++)
            {
                //使用clone方法复制参数列表中的参数
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
            }
            return clonedParms;
        }

        /// <summary>
        /// 创建SQL参数
        /// </summary>
        /// <param name="parameterName">SqlParameter名称</param>
        /// <param name="parameterValue">数据值</param>
        /// <returns>SQL参数</returns>
        public IDataParameter CreateInParameter(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }

        /// <summary>
        /// 创建输入SQL参数
        /// </summary>
        /// <param name="parameterName">SqlParameter名称</param>
        /// <param name="dbType">数据值类型</param>
        /// <param name="dbSize">数据值大小</param>
        /// <returns>SQL输入参数</returns>
        public IDataParameter CreateInParameter(string parameterName, DbType dbType, int dbSize)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.DbType = dbType;
            param.Size = dbSize;
            return param;
        }

        /// <summary>
        /// 创建SQL输出参数
        /// </summary>
        /// <param name="parameterName">SqlParameter名称</param>
        /// <param name="dbType">数据值类型</param>
        /// <param name="dbSzie">数据大小</param>
        /// <returns>SQL输出参数</returns>
        public IDataParameter CreateOutParameter(string parameterName, DbType dbType, int dbSzie)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.DbType = dbType;
            param.Size = dbSzie;
            param.Direction = ParameterDirection.Output;
            return param;
        }

        /// <summary>
        /// 创建SQL输入输出参数
        /// </summary>
        /// <param name="parameterName">SqlParameter名称</param>
        /// <param name="dbType">数据值类型</param>
        /// <param name="dbSzie">数据值大小</param>
        /// <returns>SQL输入输出参数</returns>
        public IDataParameter CreateInOutParameter(string parameterName, DbType dbType, int dbSzie)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = parameterName;
            param.DbType = dbType;
            param.Size = dbSzie;
            param.Direction = ParameterDirection.InputOutput;
            return param;
        }
        #endregion

        #region 双数据库异步执行ExecuteNonQuery方法，创建人：陈亮，创建日期：2011年11月17日
        /// <summary>
        /// 双数据库异步执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程
        /// (第一个数据库连接同步执行，第二个数据库异步执行)
        /// </summary>
        /// <param name="trans">数据库访问事务</param>
        /// <param name="connectionStringB">双数据库连接字符串列表B</param>
        /// <param name="sentence">SQL命令或存储过程名</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>影响行数（仅返回第一个同步执行的影响行数）</returns>
        public int DoubleExecuteNonQuery(IDbTransaction trans, string connectionStringB, CommandType cmdType, string sentence, params IDataParameter[] parameters)
        {
            int ReturnInt = 0;
            ReturnInt = ExecuteNonQuery(trans, cmdType, sentence, parameters);
            AsynchronousExecuteNonQuery(connectionStringB, cmdType, sentence, parameters);
            return ReturnInt;
        }
        /// <summary>
        /// 双数据库异步执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程
        /// (第一个数据库连接同步执行，第二个数据库异步执行)
        /// </summary>
        /// <param name="connectionStringA">双数据库连接字符串列表A</param>
        /// <param name="connectionStringB">双数据库连接字符串列表B</param>
        /// <param name="sentence">SQL命令或存储过程名</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>影响行数（仅返回第一个同步执行的影响行数）</returns>
        public int DoubleExecuteNonQuery(string connectionStringA, string connectionStringB, CommandType cmdType, string sentence, params IDataParameter[] parameters)
        {
            int ReturnInt = 0;
            ReturnInt = ExecuteNonQuery(connectionStringA, cmdType, sentence, parameters);
            AsynchronousExecuteNonQuery(connectionStringB, cmdType, sentence, parameters);         
            return ReturnInt;
        }

        /// <summary>
        /// 双数据库异步执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程
        /// (第一个数据库连接同步执行，第二个数据库异步执行)
        /// </summary>
        /// <param name="connectionStringA">双数据库连接字符串列表A</param>
        /// <param name="connectionStringB">双数据库连接字符串列表B</param>
        /// <param name="sentence">SQL命令或存储过程名</param>
        /// <returns>影响的行数</returns>
        public int DoubleExecuteNonQuery(string connectionStringA, string connectionStringB, CommandType cmdType, string sentence)
        {
            return DoubleExecuteNonQuery(connectionStringA, connectionStringB, cmdType, sentence, null);
        }

        /// <summary>
        /// 异步更新数据库方法，执行INSERT、UPDATE、DELETE以及不返回数据集的存储过程，必须是SqlServer数据库
        /// 多数据库更新时，第二个或以上数据库的单数据库更新方法。
        /// 利用BeginExecuteNonQuery，和异步操作回调函数AsyncCallback完成异步。
        /// 注意：建立数据库连接不要嵌套在using中，否者无法完成异步更新，在回调函数中，手动释放数据库连接资源。
        /// </summary>
        /// <param name="connectionString">异步操作数据库连接字符串</param>
        /// <param name="cmdType">操作字符串类型</param>
        /// <param name="cmdText">SQL命令操作字符串</param>
        /// <param name="commandParameters">SQL参数数组</param>
        public void AsynchronousExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params IDataParameter[] commandParameters)
        {
            //判断处理数据库连接字符串，用于支持异步更新
            if (!connectionString.Contains("Asynchronous Processing=true"))
            {
                if (connectionString.EndsWith(";"))
                {
                    connectionString += "Asynchronous Processing=true";
                }
                else
                {
                    connectionString += ";Asynchronous Processing=true";
                }
            }
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = connectionString;

            SqlCommand sqlCommand = new SqlCommand();
            //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
            PrepareCommand(sqlCommand, sqlConnection, null, cmdType, cmdText, commandParameters);
            //int val = cmd.ExecuteNonQuery();
            //清空SqlCommand中的参数列表
            //cmd.Parameters.Clear();


            //SqlCommand sqlCommand = sqlConnection.CreateCommand();
            //sqlCommand.CommandText = cmdText;
            //操作超时时间，目前设置为8分钟
            //sqlCommand.CommandTimeout = 8 * 60;
            //if (commandParameters != null)
            //{
            //    sqlCommand.Parameters.AddRange(commandParameters);
            //}
            //sqlConnection.Open();
            //执行异步更新数据库
            AsyncCallback callback = new AsyncCallback(HandleCallback);
            sqlCommand.BeginExecuteNonQuery(callback, sqlCommand);  
        }

        /// <summary>
        /// 异步更新数据库回调方法
        /// </summary>
        /// <param name="result">数据库执行的一个Transact-SQL语句或存储过程</param>
        public void HandleCallback(IAsyncResult result)
        {
            try
            {
                SqlCommand command = (SqlCommand)result.AsyncState;
                if (result.IsCompleted)
                {
                    command.EndExecuteNonQuery(result);
                }
                else
                {
                    //此处可写数据库异步操作提交失败日志
                }
                if (command.Connection != null && command.Connection.State != ConnectionState.Closed)
                {
                    command.Connection.Close();
                    command.Connection.Dispose();
                }
            }
            catch (Exception)
            { }
            finally
            { }
        }
        #endregion
        #endregion
    }
}
