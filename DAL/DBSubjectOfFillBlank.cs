using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;

namespace DAL
{
    public class DBSubjectOfFillBlank : DBOperation<SubjectOfFillBlank>
    {
        #region DBOperation<SubjectOfFillBlank> Members

        public void Insert(SubjectOfFillBlank obj)
        {
            string sql = "insert into T_SubjectOfFillBlank(question,answer,cateID) values(@question,@answer,@cateID)";
            SqlParameter[] parms ={
                                     new SqlParameter("@question",obj.Question),
                                     new SqlParameter("@answer",obj.Answer),
                                     new SqlParameter("@cateID",obj.Category.CateID)
                                 };

            DBHelper2.Insert(sql, parms);
        }

        public void Update(SubjectOfFillBlank obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            string sql = "delete from T_SubjectOfFillBlank where id=@id";
            SqlParameter[] parms ={
                                     new SqlParameter("@id",id)};
            DBHelper2.Delete(sql, parms);
        }

        public SubjectOfFillBlank SelectByID(string id)
        {
            throw new NotImplementedException();
        }
        public List<SubjectOfFillBlank> SelectList()
        {
            List<SubjectOfFillBlank> list = new List<SubjectOfFillBlank>();

            string sql = "select t1.*,t2.cateName from T_SubjectOfFillBlank  t1 left join T_SubjectTypeCategory t2 on t1.cateID=t2.cateID order by t1.cateID";
            using (SqlDataReader dr = DBHelper2.Select(sql, null))
            {
                while (dr.Read())
                {
                    SubjectOfFillBlank subject = new SubjectOfFillBlank();
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.Answer = dr["answer"].ToString();
                    subject.Category = new SubjectTypeCategory() { CateName = dr["cateName"].ToString() };
                    list.Add(subject);
                }
            }

            return list;
        }

        public List<SubjectOfFillBlank> SelectList(int cateID)
        {
            List<SubjectOfFillBlank> list = new List<SubjectOfFillBlank>();

            string sql = "select t1.*,t2.cateName from T_SubjectOfFillBlank  t1 left join T_SubjectTypeCategory t2 on t1.cateID=t2.cateID where t1.cateID=@cateID";
            SqlParameter[] parms ={
                                     new SqlParameter("@cateID",cateID)};
            using (SqlDataReader dr = DBHelper2.Select(sql, parms))
            {
                while (dr.Read())
                {
                    SubjectOfFillBlank subject = new SubjectOfFillBlank();
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.Answer = dr["answer"].ToString();
                    subject.Category = new SubjectTypeCategory() { CateName = dr["cateName"].ToString() };
                    list.Add(subject);
                }
            }

            return list;
        }

        public List<SubjectOfFillBlank> SelectList(List<int> idList)
        {
            List<SubjectOfFillBlank> list = new List<SubjectOfFillBlank>();

            string sql = "select * from T_SubjectOfFillBlank where id in(";
            foreach (int id in idList)
            {
                sql += id + ",";
            }
            sql= sql.Remove(sql.Length - 1);
            sql += ")";
            using (SqlDataReader dr = DBHelper2.Select(sql, null))
            {
                while (dr.Read())
                {
                    SubjectOfFillBlank subject = new SubjectOfFillBlank();
                    subject.Index = list.Count + 1;
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.Answer = dr["answer"].ToString();

                    list.Add(subject);
                }
            }

            return list;
        }
        #endregion
    }
}
