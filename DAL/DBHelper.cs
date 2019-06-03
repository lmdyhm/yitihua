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
    /// 封装执行存储过程的操作
    /// </summary>
   public class DBHelper
    {
       public static readonly string connStr = ConfigurationManager.ConnectionStrings["sqlserverConnStr"].ConnectionString;

       public  static int ExecuteNonQuery(string procName, params SqlParameter[] parms)
       {
           return SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, procName, parms);
       }

       public static int Insert(string procName,params SqlParameter[] parms)
       {
           return ExecuteNonQuery(procName, parms);
       }

       public static int Update(string procName, params SqlParameter[] parms)
       {
           return ExecuteNonQuery(procName, parms);
       }

       public static int Delete(string procName, params SqlParameter[] parms)
       {
           return ExecuteNonQuery(procName, parms);
       }

       public static SqlDataReader Select(string procName, params SqlParameter[] parms)
       {
           return SqlHelper.ExecuteReader(connStr, CommandType.StoredProcedure, procName, parms);
       }

       //-------------------------使用用户创建的connection
       public static int ExecuteNonQuery(SqlConnection conn, string procName, params SqlParameter[] parms)
       {
           return SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, procName, parms);
       }

       public static int Insert(SqlConnection conn, string procName, params SqlParameter[] parms)
       {
           return ExecuteNonQuery(conn,procName, parms);
       }

       public static int Update(SqlConnection conn, string procName, params SqlParameter[] parms)
       {
           return ExecuteNonQuery(conn, procName, parms);
       }

       public static int Delete(SqlConnection conn, string procName, params SqlParameter[] parms)
       {
           return ExecuteNonQuery(conn, procName, parms);
       }

       public static SqlDataReader Select(SqlConnection conn, string procName, params SqlParameter[] parms)
       {
           return SqlHelper.ExecuteReader(conn, CommandType.StoredProcedure, procName, parms);
       }

       //-------------------------使用用户创建的事务
       public static int ExecuteNonQuery(SqlTransaction trans, string procName, params SqlParameter[] parms)
       {
           return SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, procName, parms);
       }

       public static int Insert(SqlTransaction trans, string procName, params SqlParameter[] parms)
       {
           return ExecuteNonQuery(trans, procName, parms);
       }

       public static int Update(SqlTransaction trans, string procName, params SqlParameter[] parms)
       {
           return ExecuteNonQuery(trans, procName, parms);
       }

       public static int Delete(SqlTransaction trans, string procName, params SqlParameter[] parms)
       {
           return ExecuteNonQuery(trans, procName, parms);
       }

       public static SqlDataReader Select(SqlTransaction trans, string procName, params SqlParameter[] parms)
       {
           return SqlHelper.ExecuteReader(trans, CommandType.StoredProcedure, procName, parms);
       }

    }
}
