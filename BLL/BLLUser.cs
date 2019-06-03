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
        protected static readonly DBUser dbUser=new DBUser();//�������ݷ��ʲ�
       
        /// <summary>
        /// �˺Ż���������󷵻�null
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
        /// ����-1��ʾԭ������ڡ�����1��ʾ�޸ĳɹ�
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
        /// ����-1���˺��Ѵ��ڣ�����0��ʾ������󣬷���1��ע��ɹ�
        /// </summary>
        public int Register(User user)
        {
            //������֤
            if (user.UserID.Trim() == "")
                return 0;
            if (user.UserPwd.Trim() == "")
                return 0;
            if(user.Name.Trim()=="")
                return 0;
            user.UserPwd = Tool.StringHelper.EncryptPwd(user.UserPwd);//�������
            return dbUser.Register(user);
        }
    }
}
