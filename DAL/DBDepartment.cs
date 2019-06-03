//  @ Project : TestOnline
//  @ File Name : DBDepartment.cs
//  @ Date : 2009-3-12
//  @ Author : ruimingde

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;

namespace DAL
{
    public class DBDepartment : DBOperation<Department>
    {
        public void Insert(Department obj)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@deptID", SqlDbType.SmallInt,2),
					new SqlParameter("@deptName", SqlDbType.NVarChar,50)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = obj.DeptName;

            DBHelper.Insert("UP_T_Department_ADD", parameters);
        }
        public void Update(Department obj)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@deptID", SqlDbType.SmallInt,2),
					new SqlParameter("@deptName", SqlDbType.NVarChar,50)};
            parameters[0].Value = obj.DeptID;
            parameters[1].Value = obj.DeptName;

            DBHelper.Update("UP_T_Department_Update", parameters);
        }
        public void Delete(string id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@deptID", SqlDbType.SmallInt)};
            parameters[0].Value = id;

            DBHelper.Delete("UP_T_Department_Delete", parameters);
        }
        public Department SelectByID(string id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@deptID", SqlDbType.SmallInt)};
            parameters[0].Value = id;

            Department dept=new Department();

            using(SqlDataReader dr=DBHelper.Select("UP_T_Department_GetModel",parameters))
            {
                if (dr.Read())
                {
                    dept.DeptID =Convert.ToInt16(id);
                    dept.DeptName = dr.GetString(dr.GetOrdinal("deptName"));
                }
            }

            return dept;
        }
        public List<Department> SelectList()
        {
            List<Department> deptList=new List<Department>();

            using (SqlDataReader dr = DBHelper.Select("UP_T_Department_GetList", null))
            {
                while (dr.Read())
                {
                    Department dept = new Department();
                    dept.DeptID = Convert.ToInt16(dr["deptID"]);
                    dept.DeptName = dr["deptName"].ToString();
                    dept.UserCount = Convert.ToInt32(dr["userCount"]);
                    deptList.Add(dept);
                }
            }

            return deptList;
        }
    }
}
