using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;

namespace DAL
{
    public class DBSubjectOfSimpleAnswer : DBOperation<SubjectOfSimpleAnswer>
    {
        #region DBOperation<SubjectOfSimpleAnswer> Members

        public void Insert(SubjectOfSimpleAnswer obj)
        {
            string sql = "insert into T_SubjectOfSimpleAnswer(question,answer,cateID) values(@question,@answer,@cateID)";
            SqlParameter[] parms ={
                                     new SqlParameter("@question",obj.Question),
                                     new SqlParameter("@answer",obj.Answer),
                                     new SqlParameter("@cateID",obj.Category.CateID)
                                 };
            DBHelper2.Insert(sql, parms);
        }

        public void Update(SubjectOfSimpleAnswer obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            string sql = "delete from T_SubjectOfSimpleAnswer where id=@id";
            SqlParameter[] parms ={
                                     new SqlParameter("@id",id)};
            DBHelper2.Delete(sql, parms);
        }

        public SubjectOfSimpleAnswer SelectByID(string id)
        {
            throw new NotImplementedException();
        }

        public List<SubjectOfSimpleAnswer> SelectList()
        {
            List<SubjectOfSimpleAnswer> list = new List<SubjectOfSimpleAnswer>();
            string sql = "select t1.*,t2.cateName from T_SubjectOfSimpleAnswer t1 left join T_SubjectTypeCategory t2 on t1.cateID=t2.cateID order by t1.cateID";
            using (SqlDataReader dr = DBHelper2.Select(sql, null))
            {
                while (dr.Read())
                {
                    SubjectOfSimpleAnswer subject = new SubjectOfSimpleAnswer();
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.Answer = dr["answer"].ToString();
                    subject.Category = new SubjectTypeCategory() { CateName = dr["cateName"].ToString() };
                    list.Add(subject);
                }
            }

            return list;
        }

        public List<SubjectOfSimpleAnswer> SelectList(int cateID)
        {
            List<SubjectOfSimpleAnswer> list = new List<SubjectOfSimpleAnswer>();
            string sql = "select t1.*,t2.cateName from T_SubjectOfSimpleAnswer t1 left join T_SubjectTypeCategory t2 on t1.cateID=t2.cateID where t1.cateID=@cateID";
            SqlParameter[] parms ={
                                      new SqlParameter("@cateID",cateID)};
            using (SqlDataReader dr = DBHelper2.Select(sql, parms))
            {
                while (dr.Read())
                {
                    SubjectOfSimpleAnswer subject = new SubjectOfSimpleAnswer();
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.Answer = dr["answer"].ToString();
                    subject.Category = new SubjectTypeCategory() { CateName = dr["cateName"].ToString() };
                    list.Add(subject);
                }
            }

            return list;
        }

        public List<SubjectOfSimpleAnswer> SelectList(List<int> idList)
        {
            List<SubjectOfSimpleAnswer> list = new List<SubjectOfSimpleAnswer>();
            string sql = "select * from T_SubjectOfSimpleAnswer where id in(";
            foreach (int id in idList)
            {
                sql += id + ",";
            }
            sql = sql.Remove(sql.Length - 1);
            sql += ")";

            using (SqlDataReader dr = DBHelper2.Select(sql, null))
            {
                while (dr.Read())
                {
                    SubjectOfSimpleAnswer subject = new SubjectOfSimpleAnswer();
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
