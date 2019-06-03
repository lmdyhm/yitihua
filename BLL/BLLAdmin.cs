//  @ Project : TestOnline
//  @ File Name : Admin.cs
//  @ Date : 2009-3-12
//  @ Author : ruimingde
//  @ download link: http://www.51aspx.com
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;
using Tool;

namespace BLL
{
    public class BLLAdmin : BLLUser
    {
        public void CreateUser(User user)
        {
            if (user.Department.DeptID == -1)
                return;
            if (user.RightList.Count == 0)
                return;
            user.UserPwd = Tool.StringHelper.EncryptPwd(user.UserPwd);//密码加密
            dbUser.Insert(user);
        }
        public List<User> GetUserListByDeptID(int deptID)
        {
            return dbUser.SelectListByDeptID((short)deptID);
        }

        public PageList<User> GetUserListByDeptID(int deptID, int pageNum, int pageSize)
        {
            return new PageList<User>(dbUser.SelectListByDeptID((short)deptID), pageNum, pageSize);
        }

        public const string DEFAULT_ADMIN = "admin";
        public void RemoveUser(string userID)
        {
            if (string.IsNullOrEmpty(userID))
                throw new BLLException("参数userID为空！");

            if (DEFAULT_ADMIN.Equals(userID.ToLower()))
                throw new BLLException(string.Format("默认管理员{0}无法删除！", DEFAULT_ADMIN));

            dbUser.Delete(userID);
        }
        public void ModifyUser(User user)
        {
            if (null == user || string.IsNullOrEmpty(user.UserID))
                throw new BLLException("user对象为空，或者userID为空！");

            if (DEFAULT_ADMIN.Equals(user.UserID.ToLower()))
            {
                if (user.Locked)
                    throw new BLLException(string.Format("{0}不能修改为锁定！", user.UserID));

                bool isAdmin = false;
                foreach (string right in user.RightList)
                {
                    if (right.Equals("管理员"))
                        isAdmin = true;
                }

                if (!isAdmin)
                    throw new BLLException(string.Format("{0}的权限必须含有管理员!", user.UserID));
            }

            dbUser.Update(user);
        }


        public bool CheckUserIDIsExist(string userID)
        {
            if (null == dbUser.SelectByID(userID))
                return false;
            else
                return true;
        }

        #endregion

        public const string DEFAULT_PWD = "123456";
        public void ResetPwd(string userID)
        {
            dbUser.ResetPwd(userID, StringHelper.EncryptPwd(DEFAULT_PWD));
        }

        public List<User> GetTesterByTestID(string testID)
        {
            int id = 0;
            if (!int.TryParse(testID, out id))
                throw new BLLException("参数testID不是整数！");

            return dbUser.SelectTesterByTestID(id);
        }
    }
}
