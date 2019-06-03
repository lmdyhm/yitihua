//  @ Project : TestOnline
//  @ File Name : DBPaper.cs
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
    public class DBPaper : DBOperation<Paper>
    {
        public void Insert(Paper obj)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@paperName", SqlDbType.NVarChar,50),
					new SqlParameter("@paperType", SqlDbType.NVarChar,30),
					new SqlParameter("@content", SqlDbType.Text),
					new SqlParameter("@answer", SqlDbType.Text),
					new SqlParameter("@creator", SqlDbType.NVarChar,10),
					new SqlParameter("@createdTime", SqlDbType.SmallDateTime),
                    new SqlParameter("@deptID",SqlDbType.SmallInt,2)};
            parameters[0].Value = obj.PaperName;
            parameters[1].Value = obj.PaperType.DeptName;
            parameters[2].Value = obj.Content;
            parameters[3].Value = obj.Answer;
            parameters[4].Value = obj.Creator.Name;
            parameters[5].Value = obj.CreatedTime;
            parameters[6].Value = obj.PaperType.DeptID;

            DBHelper.Insert("UP_T_Paper_ADD", parameters);
        }

        public void Update(Paper obj)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("paperID",SqlDbType.Int,4),
					new SqlParameter("@paperName", SqlDbType.NVarChar,50),
					new SqlParameter("@paperType", SqlDbType.NVarChar,30),
					new SqlParameter("@content", SqlDbType.Text),
					new SqlParameter("@answer", SqlDbType.Text),
					new SqlParameter("@creator", SqlDbType.NVarChar,10)};
            parameters[0].Value = obj.PaperID;
            parameters[1].Value = obj.PaperName;
            parameters[2].Value = obj.PaperType.DeptName;
            parameters[3].Value = obj.Content;
            parameters[4].Value = obj.Answer;
            parameters[5].Value = obj.Creator.Name;

            DBHelper.Update("UP_T_Paper_Update", parameters);
        }

        public void Delete(string paperID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@paperID", SqlDbType.Int,4)};
            parameters[0].Value = paperID;

            DBHelper.Delete("UP_T_Paper_Delete", parameters);
        }

        public Paper SelectByID(string paperID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@paperID", SqlDbType.Int,4)};
            parameters[0].Value = paperID;

            Paper paper = null;

            using (SqlDataReader dr = DBHelper.Select("UP_T_Paper_GetModel", parameters))
            {
                if (dr.Read())
                {
                    paper = new Paper();
                    paper.PaperID = Convert.ToInt32(paperID);
                    paper.PaperName = dr.GetString(dr.GetOrdinal("paperName"));
                    Department dept = new Department();
                    dept.DeptName = dr.GetString(dr.GetOrdinal("paperType"));
                    paper.PaperType = dept;
                    paper.Answer = dr.GetString(dr.GetOrdinal("answer"));
                    paper.Content = dr.GetString(dr.GetOrdinal("content"));
                    paper.CreatedTime = dr.GetDateTime(dr.GetOrdinal("createdTime"));
                    Admin creator = new Admin();
                    creator.Name = dr["creator"].ToString();
                    paper.Creator = creator;
                }
            }
            return paper;
        }

        public List<Paper> SelectList()
        {
            List<Paper> paperList = new List<Paper>();

            using (SqlDataReader dr = DBHelper.Select("UP_T_Paper_GetList", null))
            {
                while(dr.Read())
                {
                    Paper paper = new Paper();
                    paper.PaperID =dr.GetInt32(dr.GetOrdinal("paperID"));
                    paper.PaperName = dr.GetString(dr.GetOrdinal("paperName"));
                    Department dept = new Department();
                    dept.DeptName = dr.GetString(dr.GetOrdinal("paperType"));
                    paper.PaperType = dept;
                    paper.CreatedTime = dr.GetDateTime(dr.GetOrdinal("createdTime"));
                    Admin creator = new Admin();
                    creator.Name = dr["creator"].ToString();
                    paper.Creator = creator;

                    paperList.Add(paper);
                }
            }
            return paperList;
        }

        public List<Paper> SelectListByDeptID(int deptID)
        {
            SqlParameter[] parms={
                new SqlParameter("@deptID",SqlDbType.Int)};
            parms[0].Value = deptID;

            List<Paper> paperList = new List<Paper>();

            using (SqlDataReader dr = DBHelper.Select("UP_T_Paper_GetListByDeptID", parms))
            {
                while (dr.Read())
                {
                    Paper paper = new Paper();
                    paper.PaperID = dr.GetInt32(dr.GetOrdinal("paperID"));
                    paper.PaperName = dr.GetString(dr.GetOrdinal("paperName"));
                    Department dept = new Department();
                    dept.DeptName = dr.GetString(dr.GetOrdinal("paperType"));
                    paper.PaperType = dept;
                    paper.CreatedTime = dr.GetDateTime(dr.GetOrdinal("createdTime"));
                    Admin creator = new Admin();
                    creator.Name = dr.GetString(dr.GetOrdinal("creator"));
                    paper.Creator = creator;

                    paperList.Add(paper);
                }
            }
            return paperList;
        }
    }
}
