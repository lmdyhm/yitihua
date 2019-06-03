using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Tool
{
    public class StringHelper
    {

        /// <summary>
        /// 返回："abc,aaa,ddd"
        /// </summary>
        /// <param name="strList"></param>
        /// <returns></returns>
        public static string GetStrList(List<string> strList)
        {
            if (null == strList)
                return string.Empty;

            string newStrList = "";
            foreach (string str in strList)
            {
                newStrList += str + ",";
            }
            return newStrList.Substring(0, newStrList.Length - 1);
        }

        public static string GetStrList(object obj)
        {
            return GetStrList((List<string>)obj);
        }

        /// <summary>
        /// str:格式为："abc,aaa,ddd
        /// </summary>
        /// <param name="str">"</param>
        /// <returns></returns>
        public static List<string> CreateStrList(string str)
        {
            List<string> strList = new List<string>();
            string[] strArray=str.Split(new char[] { ',' });

            strList.AddRange(strArray);

            return strList;
        }

        public static string GetSubString(string str, int num)
        {
            if (str.Length <= num)
                return str;
            else
                return str.Substring(0, num) + "...";
        }

        public static string FormatDateTime(object dt)
        {
            return string.Format("{0:yyyy-MM-dd}", dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">123.txt</param>
        /// <returns>yyyymmddhhmmhhmm.txt</returns>
        public static string NewFileName(string fileName)
        {
            string fileExtension = fileName.Substring(fileName.LastIndexOf('.'));
            DateTime dt = DateTime.Now;
            string newFileName = dt.Year + "" + dt.Month + "" + dt.Day + "" + dt.Hour + "" + dt.Minute + "" + dt.Second + "" + dt.Millisecond + fileExtension;

            return newFileName;
        }

        /// <summary>
        /// MD5 32为加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string EncryptPwd(string pwd)
        {
            MD5 md5 = MD5.Create();
            byte[] bStr = md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));

            string pwdEncrypted = "";
            for (int i = 0; i < bStr.Length; i++)
            {
                pwdEncrypted += bStr[i].ToString("x");
            }
            return pwdEncrypted;
        }

        /// <summary>
        /// url:index.aspx?pageNum=  或 index.aspx?cateID=1&pageNum=
        /// </summary>
        public static string MakePageUrl(string url, int pageNum, int pageCount, int recorderCount)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pageCount; i++)
            {
                if ((i + 1) == pageNum)
                    sb.Append(string.Format("{0}&nbsp;&nbsp;&nbsp;", pageNum));
                else
                    sb.Append(string.Format("<a href='{0}{1}' > {2} </a>&nbsp;&nbsp;&nbsp;", url, i + 1, i + 1));
            }
            sb.Append(string.Format(" 共{0}页，共{1}条记录", pageCount, recorderCount));

            return sb.ToString();
        }
    }
}
