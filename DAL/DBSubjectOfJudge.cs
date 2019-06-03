using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;

namespace DAL
{
    public class DBSubjectOfJudge : DBOperation<SubjectOfJudge>
    {
        #region DBOperation<SubjectOfJudge> Members

        public void Insert(SubjectOfJudge obj)
        {
            string sql = "insert into T_SubjectOfJudge(question,answer,cateID) values(@question,@answer,@cateID)";
            SqlParameter[] parms ={
                                     new SqlParameter("@question",obj.Question),
                                     new SqlParameter("@answer",obj.Answer),
                                     new SqlParameter("@cateID",obj.Category.CateID)
                                 };

            DBHelper2.Insert(sql, parms);
        }

        public void Update(SubjectOfJudge obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            string sql = "delete from T_SubjectOfJudge where id=@id";
            SqlParameter[] parms ={
                                     new SqlParameter("@id",id)};
            DBHelper2.Delete(sql, parms);
        }

        public SubjectOfJudge SelectByID(string id)
        {
            throw new NotImplementedException();
        }

        public List<SubjectOfJudge> SelectList()
        {
            List<SubjectOfJudge> list = new List<SubjectOfJudge>();

            string sql = "select t1.*,t2.cateName from T_SubjectOfJudge t1 left join T_SubjectTypeCategory t2 on t1.cateID=t2.cateID order by t1.cateID";
            using (SqlDataReader dr = DBHelper2.Select(sql, null))
            {
                while (dr.Read())
                {
                    SubjectOfJudge subject = new SubjectOfJudge();
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.Answer = Convert.ToBoolean(dr["answer"]);
                    subject.Category = new SubjectTypeCategory() { CateName = dr["cateName"].ToString() };
                    list.Add(subject);
                }
            }

            return list;
        }

        public List<SubjectOfJudge> SelectList(int cateID)
        {
            List<SubjectOfJudge> list = new List<SubjectOfJudge>();

            string sql = "select t1.*,t2.cateName from T_SubjectOfJudge t1 left join T_SubjectTypeCategory t2 on t1.cateID=t2.cateID where t1.cateID=@cateID";
            SqlParameter[] parms ={
                                     new SqlParameter("@cateID",cateID)};
            using (SqlDataReader dr = DBHelper2.Select(sql, parms))
            {
                while (dr.Read())
                {
                    SubjectOfJudge subject = new SubjectOfJudge();
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.Answer = Convert.ToBoolean(dr["answer"]);
                    subject.Category = new SubjectTypeCategory() { CateName = dr["cateName"].ToString() };
                    list.Add(subject);
                }
            }

            return list;
        }

        public List<SubjectOfJudge> SelectList(List<int> idList)
        {
            List<SubjectOfJudge> list = new List<SubjectOfJudge>();

            string sql = "select * from T_SubjectOfJudge where id in(";
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
                    SubjectOfJudge subject = new SubjectOfJudge();
                    subject.Index = list.Count + 1;
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.Answer = Convert.ToBoolean(dr["answer"]);

                    list.Add(subject);
                }
            }

            return list;
        }

        public int SelectCount()
        {
            int count = 0;
            string sql = "select count(*)  from T_SubjectOfJudge";
            using (SqlDataReader dr = DBHelper2.Select(sql, null))
            {
                if (dr.Read())
                {
                    count = Convert.ToInt32(dr[0]);
                }
            }
            return count;
        }
        public int SelectCount(int cateID)
        {
            int count = 0;
            string sql = "select count(*)  from T_SubjectOfJudge where cateID=@cateID";
            SqlParameter[] parm = { new SqlParameter("@cateID", cateID) };
            using (SqlDataReader dr = DBHelper2.Select(sql, parm))
            {
                if (dr.Read())
                {
                    count = Convert.ToInt32(dr[0]);
                }
            }
            return count;
        }
        #endregion
    }
}
