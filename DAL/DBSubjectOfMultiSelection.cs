using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;

namespace DAL
{
    public class DBSubjectOfMultiSelection : DBOperation<SubjectOfMultiSelection>
    {
        #region DBOperation<SubjectOfMultiSelection> Members

        public void Insert(SubjectOfMultiSelection obj)
        {
            string sql = "insert into T_SubjectOfMultiSelection(question,selectA,selectB,selectC,selectD,answer,cateID) values(@question,@selectA,@selectB,@selectC,@selectD,@answer,@cateID)";
            SqlParameter[] parms ={
                                     new SqlParameter("@question",obj.Question),
                                     new SqlParameter("@selectA",obj.SelectA),
                                     new SqlParameter("@selectB",obj.SelectB),
                                     new SqlParameter("@selectC",obj.SelectC),
                                     new SqlParameter("@selectD",obj.SelectD),
                                     new SqlParameter("@answer",obj.Answer),
                                     new SqlParameter("@cateID",obj.Category.CateID)
                                 };
            DBHelper2.Insert(sql, parms);
        }

        public void Update(SubjectOfMultiSelection obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            string sql = "delete from T_SubjectOfMultiSelection where id=@id";
            SqlParameter[] parms ={
                                     new SqlParameter("@id",id)};
            DBHelper2.Delete(sql, parms);
        }

        public SubjectOfMultiSelection SelectByID(string id)
        {
            throw new NotImplementedException();
        }

        public List<SubjectOfMultiSelection> SelectList()
        {
            List<SubjectOfMultiSelection> list = new List<SubjectOfMultiSelection>();

            string sql = "select t1.*,t2.cateName from T_SubjectOfMultiSelection t1 left join T_SubjectTypeCategory t2 on t1.cateID=t2.cateID order by t1.cateID";
            using (SqlDataReader dr = DBHelper2.Select(sql, null))
            {
                while (dr.Read())
                {
                    SubjectOfMultiSelection subject = new SubjectOfMultiSelection();
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.SelectA = dr["selectA"].ToString();
                    subject.SelectB = dr["selectB"].ToString();
                    subject.SelectC = dr["selectC"].ToString();
                    subject.SelectD = dr["selectD"].ToString();
                    subject.Answer = dr["answer"].ToString();
                    subject.Category = new SubjectTypeCategory() { CateName = dr["cateName"].ToString() };
                    list.Add(subject);
                }
            }

            return list;
        }

        public List<SubjectOfMultiSelection> SelectList(int cateID)
        {
            List<SubjectOfMultiSelection> list = new List<SubjectOfMultiSelection>();

            string sql = "select t1.*,t2.cateName from T_SubjectOfMultiSelection t1 left join T_SubjectTypeCategory t2 on t1.cateID=t2.cateID where t1.cateID=@cateID";
            SqlParameter[] parms ={
                                     new SqlParameter("@cateID",cateID)};
            using (SqlDataReader dr = DBHelper2.Select(sql, parms))
            {
                while (dr.Read())
                {
                    SubjectOfMultiSelection subject = new SubjectOfMultiSelection();
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.SelectA = dr["selectA"].ToString();
                    subject.SelectB = dr["selectB"].ToString();
                    subject.SelectC = dr["selectC"].ToString();
                    subject.SelectD = dr["selectD"].ToString();
                    subject.Answer = dr["answer"].ToString();
                    subject.Category = new SubjectTypeCategory() { CateName = dr["cateName"].ToString() };
                    list.Add(subject);
                }
            }

            return list;
        }

        public List<SubjectOfMultiSelection> SelectList(List<int> idList)
        {
            List<SubjectOfMultiSelection> list = new List<SubjectOfMultiSelection>();

            string sql = "select * from T_SubjectOfMultiSelection where id in(";
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
                    SubjectOfMultiSelection subject = new SubjectOfMultiSelection();
                    subject.Index = list.Count + 1;
                    subject.Id = Convert.ToInt32(dr["id"]);
                    subject.Question = dr["question"].ToString();
                    subject.SelectA = dr["selectA"].ToString();
                    subject.SelectB = dr["selectB"].ToString();
                    subject.SelectC = dr["selectC"].ToString();
                    subject.SelectD = dr["selectD"].ToString();
                    subject.Answer = dr["answer"].ToString();

                    list.Add(subject);
                }
            }

            return list;
        }

        public int SelectCount()
        {
            int count = 0;
            string sql = "select count(*)  from T_SubjectOfMultiSelection";
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
            string sql = "select count(*)  from T_SubjectOfMultiSelection where cateID=@cateID";
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
