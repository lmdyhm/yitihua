using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using Microsoft.ApplicationBlocks.Data;

namespace DAL
{
    /// <summary>
    /// 封装执行sql语句的操作
    /// </summary>
    public class DBHelper2
    {
        public static readonly string connStr = ConfigurationManager.ConnectionStrings["sqlserverConnStr"].ConnectionString;
        
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parms)
        {
            return SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, sql, parms);
        }

        public static int Insert(string sql, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(sql, parms);
        }

        public static int Update(string sql, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(sql, parms);
        }

        public static int Delete(string sql, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(sql, parms);
        }

        public static SqlDataReader Select(string sql, params SqlParameter[] parms)
        {
            return SqlHelper.ExecuteReader(connStr, CommandType.Text, sql, parms);
        }

        //-------------------------使用用户创建的connection
        public static int ExecuteNonQuery(SqlConnection conn, string sql, params SqlParameter[] parms)
        {
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, parms);
        }

        public static int Insert(SqlConnection conn, string sql, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(conn, sql, parms);
        }

        public static int Update(SqlConnection conn, string sql, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(conn, sql, parms);
        }

        public static int Delete(SqlConnection conn, string sql, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(conn, sql, parms);
        }

        public static SqlDataReader Select(SqlConnection conn, string sql, params SqlParameter[] parms)
        {
            return SqlHelper.ExecuteReader(conn, CommandType.Text, sql, parms);
        }

        //-------------------------使用用户创建的事务
        public static int ExecuteNonQuery(SqlTransaction trans, string sql, params SqlParameter[] parms)
        {
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, parms);
        }

        public static int Insert(SqlTransaction trans, string sql, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(trans, sql, parms);
        }

        public static int Update(SqlTransaction trans, string sql, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(trans, sql, parms);
        }

        public static int Delete(SqlTransaction trans, string sql, params SqlParameter[] parms)
        {
            return ExecuteNonQuery(trans, sql, parms);
        }

        public static SqlDataReader Select(SqlTransaction trans, string sql, params SqlParameter[] parms)
        {
            return SqlHelper.ExecuteReader(trans, CommandType.Text, sql, parms);
        }
    }
}
