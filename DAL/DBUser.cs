//  @ Project : TestOnline
//  @ File Name : DBUser.cs
//  @ Date : 2009-3-12
//  @ Author : ruimingde

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;
using Tool;

namespace DAL
{
    public class DBUser : DBOperation<User>
    {
        /// <summary>
        /// 返回-1表示原密码存在、返回1表示修改成功
        /// </summary>
        public int UpdatePwd(string userID, string oldPwd, string newPwd)
        {
            SqlParameter[] parms ={
                new SqlParameter("@reutrnValue",SqlDbType.Int,4),
                new SqlParameter("@userID",SqlDbType.VarChar,30),
                new SqlParameter("@oldPwd",SqlDbType.VarChar,30),
                new SqlParameter("@newPwd",SqlDbType.VarChar,30)
            };
            parms[0].Direction = ParameterDirection.ReturnValue;
            parms[1].Value = userID;
            parms[2].Value = oldPwd;
            parms[3].Value = newPwd;

            DBHelper.ExecuteNonQuery("UP_T_User_ModifyPwd", parms);

            return (int)parms[0].Value;
        }
        public List<User> SelectListByDeptID(short deptID)
        {
            List<User> userList = new List<User>();
            SqlParameter[] parms ={
                new SqlParameter("@deptID",SqlDbType.SmallInt,2)};
            parms[0].Value = deptID;

            using (SqlDataReader dr = DBHelper.Select("UP_T_User_GetListByDeptID", parms))
            {
                while (dr.Read())
                {
                    User user = new User();
                    user.UserID = dr["userID"].ToString();
                    user.UserPwd = dr["userPwd"].ToString();
                    Department dept = new Department();
                    dept.DeptID = deptID;
                    dept.DeptName = dr["deptName"].ToString();
                    user.Department = dept;
                    user.CreatedTime =Convert.ToDateTime( dr["createdTime"]);
                    user.LastLoginIP = dr["lastLoginIP"].ToString();
                    if (!dr["lastLoginTime"].ToString().Trim().Equals(""))
                    {
                        user.LastLoginTime = Convert.ToDateTime(dr["lastLoginTime"]);
                    }
                    user.Locked =Convert.ToBoolean( dr["locked"]);
                    user.Name = dr["name"].ToString();
                    user.RightList = StringHelper.CreateStrList(dr["right"].ToString());

                    userList.Add(user);
                }
            }
            return userList;
        }
        public void Insert(User obj)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.VarChar,30),
					new SqlParameter("@name", SqlDbType.NVarChar,10),
					new SqlParameter("@userPwd", SqlDbType.VarChar,30),
					new SqlParameter("@deptID", SqlDbType.SmallInt,2),
					new SqlParameter("@right", SqlDbType.VarChar,20),
					new SqlParameter("@locked", SqlDbType.Bit,1),
					new SqlParameter("@lastLoginIP", SqlDbType.VarChar,20),
					};
            parameters[0].Value = obj.UserID;
            parameters[1].Value = obj.Name;
            parameters[2].Value = obj.UserPwd;
            parameters[3].Value = obj.Department.DeptID ;
            parameters[4].Value = StringHelper.GetStrList(obj.RightList);
            parameters[5].Value = obj.Locked;
            parameters[6].Value = obj.LastLoginIP;

            DBHelper.Insert("UP_T_User_ADD", parameters);
        }

        /// <summary>
        /// 返回-1：账号已存在，返回1：注册成功
        /// </summary>
        public int Register(User obj)
        {
            SqlParameter[] parameters = {
                                        new SqlParameter("@returnValue",0),
                                        new SqlParameter("@userID",obj.UserID),
                                        new SqlParameter("@name",obj.Name),
                                        new SqlParameter("@userPwd",obj.UserPwd),
                                        new SqlParameter("@deptID",obj.Department.DeptID),
                                        new SqlParameter("@right",StringHelper.GetStrList(obj.RightList)),
                                        new SqlParameter("@locked",obj.Locked),
                                        new SqlParameter("@sex",(int)obj.Sex),
                                        new SqlParameter("@birthDay",obj.BirthDay),
                                        new SqlParameter("@homeTown",obj.Hometown),
                                        new SqlParameter("@school",obj.School),
                                        new SqlParameter("@major",obj.Major),
                                        new SqlParameter("@studyExprience",obj.StudyExprience),
                                        new SqlParameter("@graduateDate",obj.GraduateDate),
                                        new SqlParameter("@telephone",obj.Telephone),
                                        new SqlParameter("@email",obj.Email)};
            parameters[0].Direction = ParameterDirection.ReturnValue;
            DBHelper.Insert("UP_T_User_Register", parameters);
            int returnValue = Convert.ToInt32(parameters[0].Value);
            return returnValue;
        }

        public void Update(User obj)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.VarChar,30),
					new SqlParameter("@name", SqlDbType.NVarChar,10),
					new SqlParameter("@deptID", SqlDbType.SmallInt,2),
					new SqlParameter("@right", SqlDbType.VarChar,20),
					new SqlParameter("@locked", SqlDbType.Bit,1),
            };
            parameters[0].Value = obj.UserID;
            parameters[1].Value = obj.Name;
            parameters[2].Value = obj.Department.DeptID;
            parameters[3].Value = StringHelper.GetStrList(obj.RightList);
            parameters[4].Value = obj.Locked;

            DBHelper.Insert("UP_T_User_Update", parameters);
        }
        public void Delete(string id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.VarChar,50)};
            parameters[0].Value = id;

            DBHelper.Delete("UP_T_User_Delete", parameters);
        }
        /// <summary>
        /// 不存在返回null
        /// </summary>
        public User SelectByID(string userID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.VarChar,50)};
            parameters[0].Value = userID;

            using (SqlDataReader dr=DBHelper.Select("UP_T_User_GetModel",parameters))
            {
                if (dr.Read())
                {
                    User user = new User();
                    user.UserID = userID;
                    user.UserPwd = dr["userPwd"].ToString();
                    Department dept=new Department();
                    dept.DeptID=Convert.ToInt16(dr["deptID"]);
                    dept.DeptName = dr["deptName"].ToString();
                    user.Department = dept;
                    if(!dr["createdTime"].ToString().Trim().Equals(""))
                        user.CreatedTime =Convert.ToDateTime( dr["createdTime"]);
                    user.LastLoginIP = dr["lastLoginIP"].ToString();
                    if(!dr["lastLoginTime"].ToString().Trim().Equals(""))
                        user.LastLoginTime = Convert.ToDateTime(dr["lastLoginTime"]);
                    user.Locked =Convert.ToBoolean(dr["locked"]);
                    user.Name = dr["name"].ToString();
                    user.RightList =StringHelper.CreateStrList(dr["right"].ToString());

                    return user;
                }
                else
                    return null;
            }
        }
        public List<User> SelectList()
        {
            List<User> userList = new List<User>();

            using (SqlDataReader dr = DBHelper.Select("UP_T_User_GetList", null))
            {
                while (dr.Read())
                {
                    User user = new User();
                    user.UserID = dr["userID"].ToString();
                    user.UserPwd = dr["userPwd"].ToString();
                    Department dept = new Department();
                    dept.DeptID = Convert.ToInt16(dr["deptID"]);
                    dept.DeptName = dr["deptName"].ToString();
                    user.Department = dept;
                    if (!dr["createdTime"].ToString().Trim().Equals(""))
                        user.CreatedTime = Convert.ToDateTime(dr["createdTime"]);
                    user.LastLoginIP = dr["lastLoginIP"].ToString();
                    if (!dr["lastLoginTime"].ToString().Trim().Equals(""))
                        user.LastLoginTime = Convert.ToDateTime(dr["lastLoginTime"]);
                    user.Locked = Convert.ToBoolean(dr["locked"]);
                    user.Name = dr["name"].ToString();
                    user.RightList = StringHelper.CreateStrList(dr["right"].ToString());

                    userList.Add(user);
                }
            }
            return userList;
        }

        public void ResetPwd(string userID, string pwd)
        {
            SqlParameter[] parms ={
                new SqlParameter("@userID", SqlDbType.VarChar,30),
                new SqlParameter("@pwd", SqlDbType.VarChar, 30)
            };
            parms[0].Value = userID;
            parms[1].Value = pwd;

            DBHelper.Update("UP_T_User_ResetPwd", parms);
        }

        public List<User> SelectTesterByTestID(int testID)
        {
            SqlParameter[] parm =
            {
                new SqlParameter("@testID",SqlDbType.Int,4)
            };
            parm[0].Value=testID;

            List<User> userList = new List<User>();
            using (SqlDataReader dr = DBHelper.Select("UP_T_User_SelectTesterByTestID", parm))
            {
                while (dr.Read())
                {
                    User user = new User();
                    user.UserID = dr["userID"].ToString();
                    user.Name = dr["name"].ToString();
                    user.Department.DeptName = dr["deptName"].ToString();

                    userList.Add(user);
                }
            }

            return userList;
        }
    }
}
