using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DAL
{
    public class SqlHelper
    {
        private static readonly string conn =
            ConfigurationManager.ConnectionStrings["conn"].ToString();
        public static SqlConnection connection = null;
        #region 123456
        //public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        //{
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(conn);
        //        connection.Open();
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        if (parameters != null)
        //            command.Parameters.AddRange(parameters);
        //        return command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("数据库访问发生错误:" + ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //            connection.Close();
        //    }
        //}
        //public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        //{
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(conn);
        //        connection.Open();
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        if (parameters != null)
        //            command.Parameters.AddRange(parameters);
        //        return command.ExecuteScalar();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("数据库访问发生错误:" + ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //            connection.Close();
        //    }
        //}
        //public static SqlDataReader ExecuteReader(string sql, SqlParameter[] parameters = null)
        //{
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(conn);
        //        connection.Open();
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        if (parameters != null)
        //            command.Parameters.AddRange(parameters);
        //        return command.ExecuteReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("数据库访问发生错误:" + ex.Message);
        //    }
        //}
        //public static DataSet GetDataSet(string sql, SqlParameter[] parameters = null)
        //{
        //    SqlConnection connection = null;
        //    DataSet dataSet = new DataSet();
        //    try
        //    {
        //        connection = new SqlConnection(conn);
        //        connection.Open();
        //        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
        //        if (parameters != null)
        //            adapter.SelectCommand.Parameters.AddRange(parameters);
        //        adapter.Fill(dataSet);
        //        return dataSet;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("数据库访问发生错误:" + ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //            connection.Close();
        //    }
        //}
        //public static bool ExecuteSQL(List<string> sqlList)
        //{
        //    bool success = true;
        //    SqlConnection connection = null;
        //    SqlTransaction transaction = null;
        //    try
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand();
        //        transaction = connection.BeginTransaction();
        //        command.Connection = connection;
        //        command.Transaction = transaction;
        //        foreach (var sql in sqlList)
        //        {
        //            command.CommandText = sql;
        //            command.ExecuteNonQuery();
        //        }
        //        transaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        success = false;
        //        transaction.Rollback();
        //        throw new Exception("数据库访问发生错误:" + ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null)
        //            connection.Close();
        //    }
        //    return success;
        //}

        #endregion

        /// <summary>
        /// 通用的增删改方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">如果不为空，则是使用参数化</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql,params SqlParameter[] parameters)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(conn);
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();                      
            }
            catch (Exception ex)
            {
                throw new Exception("数据库访问出错!详细消息:"+ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static int ExecuteNonQuery(string sql,bool isProc=false,params SqlParameter[] parameters)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(conn);
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                if (isProc)
                    command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库访问出错!详细消息:" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        /// <summary>
        /// 返回单行单列的结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(conn);
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("数据库访问出错!详细消息:" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        /// <summary>
        /// 返回多行多列的结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] parameters)
        {
            //SqlDataReader在数据没有被读取之前不能关掉连接
            connection = null;
            try
            {
                connection = new SqlConnection(conn);
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (connection != null)
                    connection.Close();
                throw new Exception("数据库访问出错!详细消息:" + ex.Message);
            }        
        }

        public static DataSet GetDataSet(string sql, params SqlParameter[] parameters)
        {
            SqlConnection connection = null;
            DataSet dataSet = new DataSet();
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);
                if (parameters != null)
                    dataAdapter.SelectCommand.Parameters.AddRange(parameters);
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception("数据库访问出错!详细消息:" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [ObsoleteAttribute]
        public static object ExecuteProc(string procName,params SqlParameter[] parameters)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(conn);
                connection.Open();
                SqlCommand command = new SqlCommand(procName, connection);
                //将类型改为存储过程
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                    int result = (int)command.Parameters["@age"].Value;
                }                                  
                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }


        public static bool ExecuteSQL(List<string> sqlList)
        {
            bool isSuccess = false;
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            try
            {
                connection = new SqlConnection(conn);
                connection.Open();
                //开启事务
                transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                //将事务绑定给 command对象
                command.Transaction = transaction;
                foreach (var sql in sqlList)
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                //提交事务
                transaction.Commit();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                //回滚事务
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return isSuccess;
        }
    }
}
