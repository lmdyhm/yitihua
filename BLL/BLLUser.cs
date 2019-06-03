//  @ Project : TestOnline
//  @ File Name : User.cs
//  @ Date : 2009-3-12
//  @ Author : ruimingde
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
    public class BLLUser
    {
        protected static readonly DBUser dbUser=new DBUser();//引用数据访问层
       
        /// <summary>
        /// 账号或者密码错误返回null
        /// </summary>
        public User CheckLogin(string userID, string userPwd)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(userPwd))
                return null;

            User user = dbUser.SelectByID(userID);
            if (null == user)
                return null;

            if (user.UserPwd.Equals(Tool.StringHelper.EncryptPwd(userPwd)))
            {
                return user;
            }
            else
                return null;
        }

        /// <summary>
        /// 返回-1表示原密码存在、返回1表示修改成功
        /// </summary>
        public int ModifyPwd(string userID, string oldPwd, string newPwd)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(oldPwd) ||string.IsNullOrEmpty(newPwd))
                return 0;

            oldPwd = Tool.StringHelper.EncryptPwd(oldPwd);
            newPwd = Tool.StringHelper.EncryptPwd(newPwd);

            return dbUser.UpdatePwd(userID, oldPwd, newPwd);

        }

        public User GetUserByID(string userID)
        {
            if (string.IsNullOrEmpty(userID))
                return null;

            return dbUser.SelectByID(userID);
        }

        /// <summary>
        /// 返回-1：账号已存在，返回0表示输入错误，返回1：注册成功
        /// </summary>
        public int Register(User user)
        {
            //输入验证
            if (user.UserID.Trim() == "")
                return 0;
            if (user.UserPwd.Trim() == "")
                return 0;
            if(user.Name.Trim()=="")
                return 0;
            user.UserPwd = Tool.StringHelper.EncryptPwd(user.UserPwd);//密码加密
            return dbUser.Register(user);
        }
    }
}
