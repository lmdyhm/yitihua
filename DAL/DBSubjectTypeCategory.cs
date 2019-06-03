using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;

namespace DAL
{
    public class DBSubjectTypeCategory : DBOperation<SubjectTypeCategory>
    {
        #region DBOperation<SubjectTypeCategory> Members

        public void Insert(SubjectTypeCategory obj)
        {
            string sql = "insert into T_SubjectTypeCategory(cateName,subjectType) values(@cateName,@subjectType)";
            SqlParameter[] parms ={
                                     new SqlParameter("@cateName",obj.CateName),
                                     new SqlParameter("@subjectType",(int)obj.SubjectType)};
            DBHelper2.Insert(sql, parms);
        }

        public void Update(SubjectTypeCategory obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public SubjectTypeCategory SelectByID(string id)
        {
            throw new NotImplementedException();
        }

        public List<SubjectTypeCategory> SelectList()
        {
            throw new NotImplementedException();
        }
        public List<SubjectTypeCategory> SelectListBySubjectType(SubjectType subjectType)
        {
            string sql = "select * from T_SubjectTypeCategory where subjectType=@subjectType";
            SqlParameter[] parms = { new SqlParameter("@subjectType", (int)subjectType) };
            List<SubjectTypeCategory> list = new List<SubjectTypeCategory>();
            using (SqlDataReader dr = DBHelper2.Select(sql, parms))
            {
                while (dr.Read())
                {
                    SubjectTypeCategory category = new SubjectTypeCategory();
                    category.CateID = Convert.ToInt32(dr["cateID"]);
                    category.CateName = dr["cateName"].ToString();
                    list.Add(category);
                }
                return list;
            }
        }
        #endregion
    }
}
