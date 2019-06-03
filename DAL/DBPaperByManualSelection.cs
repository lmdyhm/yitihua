using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;

namespace DAL
{
   public class DBPaperByManualSelection : DBOperation<PaperByManualSelection>
    {
        #region DBOperation<PaperByManualSelection> Members

        public void Insert(PaperByManualSelection obj)
        {
            string sql = "insert into T_PaperByManualSelection(paperName,deptID,paperType,creator,createdTime) values(@paperName,@deptID,@paperType,@creator,@createdTime); select @@identity";
            SqlParameter[] parms={
                                     new SqlParameter("@paperName",obj.PaperName),
                                      new SqlParameter("@deptID",obj.PaperType.DeptID),
                                     new SqlParameter("@paperType",obj.PaperType.DeptName),
                                     new SqlParameter("@creator",obj.Creator.Name),
                                     new SqlParameter("@createdTime",obj.CreatedTime)
                                 };
            
            SqlConnection conn=new SqlConnection(DBHelper2.connStr);
            SqlTransaction trans = null;
            try
            {
                conn.Open();
                trans=conn.BeginTransaction("TInsertPaperByManualSelection");
                using (SqlDataReader dr = DBHelper2.Select(trans, sql, parms))
                {
                    if (dr.Read())
                    {
                        int id =Convert.ToInt32( dr[0]);
                        dr.Close();
                        sql="insert into T_PaperByManualSelection_Subject(paperID,subjectType,subjectID) values(@paperID,@subjectType,@subjectID)";
                        
                        //1：填空题，2：判断题，3：单选题，4：多选题，5：简答题
                        if(obj.FillBlankList!=null)
                        foreach (SubjectOfFillBlank subject in obj.FillBlankList)
                        {
                            SqlParameter[] parms2 ={
                                new SqlParameter("@paperID",id),
                                new SqlParameter("@subjectType",1),
                                new SqlParameter("@subjectID",subject.Id)
                            };
                            DBHelper2.Insert(trans, sql, parms2);
                        }

                        if(obj.JudgeList!=null)
                        foreach (SubjectOfJudge subject in obj.JudgeList)
                        {
                            SqlParameter[] parms2 ={
                                new SqlParameter("@paperID",id),
                                new SqlParameter("@subjectType",2),
                                new SqlParameter("@subjectID",subject.Id)
                            };
                            DBHelper2.Insert(trans, sql, parms2);
                        }

                        if(obj.SingleSelectionList!=null)
                        foreach (SubjectOfSingleSelection subject in obj.SingleSelectionList)
                        {
                            SqlParameter[] parms2 ={
                                new SqlParameter("@paperID",id),
                                new SqlParameter("@subjectType",3),
                                new SqlParameter("@subjectID",subject.Id)
                            };
                            DBHelper2.Insert(trans, sql, parms2);
                        }

                        if(obj.MultiSelectionList!=null)
                        foreach (SubjectOfMultiSelection subject in obj.MultiSelectionList)
                        {
                            SqlParameter[] parms2 ={
                                new SqlParameter("@paperID",id),
                                new SqlParameter("@subjectType",4),
                                new SqlParameter("@subjectID",subject.Id)
                            };
                            DBHelper2.Insert(trans, sql, parms2);
                        }

                        if(obj.SimpleAnswerList!=null)
                        foreach (SubjectOfSimpleAnswer subject in obj.SimpleAnswerList)
                        {
                            SqlParameter[] parms2 ={
                                new SqlParameter("@paperID",id),
                                new SqlParameter("@subjectType",5),
                                new SqlParameter("@subjectID",subject.Id)
                            };
                            DBHelper2.Insert(trans, sql, parms2);
                        }
                    }
                }

                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                trans.Dispose();
                conn.Close();
            }
        }

        public void Update(PaperByManualSelection obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            string sql = "delete from T_PaperByManualSelection where paperID=@paperID";
            SqlParameter[] parms ={
                                     new SqlParameter("@paperID",id)};
            DBHelper2.Delete(sql, parms);
        }

        public PaperByManualSelection SelectByID(string id)
        {
            PaperByManualSelection paper = new PaperByManualSelection();

            string sql = "select * from T_PaperByManualSelection where paperID=@paperID";
            SqlParameter[] parms ={
                                     new SqlParameter("@paperID",id)};
            using (SqlDataReader dr = DBHelper2.Select(sql, parms))
            {
                if (dr.Read())
                {
                    paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    paper.PaperName = dr["paperName"].ToString();
                    paper.PaperType.DeptName = dr["paperType"].ToString();
                    dr.Close();
                }

                //填空题
                sql = "select t1.* from T_SubjectOfFillBlank t1 inner join T_PaperByManualSelection_Subject t2 on t1.id=t2.subjectID where  t2.paperID=@paperID and t2.subjectType=1";
                using (SqlDataReader dr2 = DBHelper2.Select(sql, parms))
                {
                    List<SubjectOfFillBlank> list = new List<SubjectOfFillBlank>();
                    while (dr2.Read())
                    {
                        SubjectOfFillBlank subject = new SubjectOfFillBlank();
                        subject.Index = list.Count + 1;
                        subject.Id = Convert.ToInt32(dr2["id"]);
                        subject.Question = dr2["question"].ToString();
                        subject.Answer = dr2["answer"].ToString();

                        list.Add(subject);
                    }
                    paper.FillBlankList = list;
                }

                //判断题
                sql = "select t1.* from T_SubjectOfJudge t1 inner join T_PaperByManualSelection_Subject t2 on t1.id=t2.subjectID where t2.paperID=@paperID and  t2.subjectType=2";
                using (SqlDataReader dr2 = DBHelper2.Select(sql, parms))
                {
                    List<SubjectOfJudge> list = new List<SubjectOfJudge>();
                    while (dr2.Read())
                    {
                        SubjectOfJudge subject = new SubjectOfJudge();
                        subject.Index = list.Count + 1;
                        subject.Id = Convert.ToInt32(dr2["id"]);
                        subject.Question = dr2["question"].ToString();
                        subject.Answer =Convert.ToBoolean(dr2["answer"]);

                        list.Add(subject);
                    }
                    paper.JudgeList = list;
                }

                //单选题
                sql = "select t1.* from T_SubjectOfSingleSelection t1 inner join T_PaperByManualSelection_Subject t2 on t1.id=t2.subjectID where t2.paperID=@paperID and  t2.subjectType=3";
                using (SqlDataReader dr2 = DBHelper2.Select(sql, parms))
                {
                    List<SubjectOfSingleSelection> list = new List<SubjectOfSingleSelection>();
                    while (dr2.Read())
                    {
                        SubjectOfSingleSelection subject = new SubjectOfSingleSelection();
                        subject.Index = list.Count + 1;
                        subject.Id = Convert.ToInt32(dr2["id"]);
                        subject.Question = dr2["question"].ToString();
                        subject.Answer =Convert.ToChar(dr2["answer"].ToString().ToUpper());
                        subject.SelectA = dr2["selectA"].ToString();
                        subject.SelectB = dr2["selectB"].ToString();
                        subject.SelectC = dr2["selectC"].ToString();
                        subject.SelectD = dr2["selectD"].ToString();

                        list.Add(subject);
                    }
                    paper.SingleSelectionList = list;
                }

                //多选题
                sql = "select t1.* from T_SubjectOfMultiSelection t1 inner join T_PaperByManualSelection_Subject t2 on t1.id=t2.subjectID where t2.paperID=@paperID and  t2.subjectType=4";
                using (SqlDataReader dr2 = DBHelper2.Select(sql, parms))
                {
                    List<SubjectOfMultiSelection> list = new List<SubjectOfMultiSelection>();
                    while (dr2.Read())
                    {
                        SubjectOfMultiSelection subject = new SubjectOfMultiSelection();
                        subject.Index = list.Count + 1;
                        subject.Id = Convert.ToInt32(dr2["id"]);
                        subject.Question = dr2["question"].ToString();
                        subject.Answer = dr2["answer"].ToString().ToUpper();
                        subject.SelectA = dr2["selectA"].ToString();
                        subject.SelectB = dr2["selectB"].ToString();
                        subject.SelectC = dr2["selectC"].ToString();
                        subject.SelectD = dr2["selectD"].ToString();

                        list.Add(subject);
                    }
                    paper.MultiSelectionList = list;
                }

                //简答题
                sql = "select t1.* from T_SubjectOfSimpleAnswer t1 inner join T_PaperByManualSelection_Subject t2 on t1.id=t2.subjectID where t2.paperID=@paperID and  t2.subjectType=5";
                using (SqlDataReader dr2 = DBHelper2.Select(sql, parms))
                {
                    List<SubjectOfSimpleAnswer> list = new List<SubjectOfSimpleAnswer>();
                    while (dr2.Read())
                    {
                        SubjectOfSimpleAnswer subject = new SubjectOfSimpleAnswer();
                        subject.Index = list.Count + 1;
                        subject.Id = Convert.ToInt32(dr2["id"]);
                        subject.Question = dr2["question"].ToString();
                        subject.Answer = dr2["answer"].ToString();

                        list.Add(subject);
                    }
                    paper.SimpleAnswerList = list;
                }
            }

            return paper;
        }

        public PaperByManualSelection SelectByID(int testID, int paperID)
        {
            PaperByManualSelection paper = SelectByID(paperID.ToString());

            SqlConnection conn=new SqlConnection(DBHelper2.connStr);
           
            try
            {
                conn.Open();

                //填空题、判断题、单选题、多选题中每题的分数
                string sql = "select top 4 * from T_Test_Subject_Scores where testID=@testID order by subjectType asc";
                SqlParameter[] parms ={
                                     new SqlParameter("@testID",testID)};
                using (SqlDataReader dr = DBHelper2.Select(conn, sql, parms))
                {
                    while (dr.Read())
                    {
                        int subjectType=Convert.ToInt32(dr["subjectType"]);
                        switch((SubjectType)subjectType)
                        {
                            case SubjectType.FillBlank:
                                foreach(SubjectOfFillBlank subject in paper.FillBlankList)
                                {
                                    subject.Scores=Convert.ToInt32(dr["scores"]);
                                }
                                break;
                            case SubjectType.Judge:
                                foreach(SubjectOfJudge subject in paper.JudgeList)
                                {
                                    subject.Scores=Convert.ToInt32(dr["scores"]);
                                }
                                break;
                            case SubjectType.SingleSelection:
                                foreach(SubjectOfSingleSelection subject in paper.SingleSelectionList)
                                {
                                    subject.Scores=Convert.ToInt32(dr["scores"]);
                                }
                                break;
                            case SubjectType.MultiSelection:
                                foreach(SubjectOfMultiSelection subject in paper.MultiSelectionList)
                                {
                                    subject.Scores=Convert.ToInt32(dr["scores"]);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    dr.Close();

                    //简答题相关的分数
                    sql="select * from  T_Test_SubjectOfSimpleAnswer_Scores where testID=@testID";
                    using(SqlDataReader dr2=DBHelper2.Select(conn,sql,parms))
                    {
                        while(dr2.Read())
                        {
                            foreach(SubjectOfSimpleAnswer subject in paper.SimpleAnswerList)
                            {
                                if(subject.Id==Convert.ToInt32(dr2["subjectID"]))
                                {
                                    subject.Scores=Convert.ToInt32(dr2["scores"]);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return paper;
        }
        
        public List<PaperByManualSelection> SelectList()
        {
            List<PaperByManualSelection> list = new List<PaperByManualSelection>();
            string sql = "select * from T_PaperByManualSelection order by paperID desc";
            using (SqlDataReader dr = DBHelper2.Select(sql, null))
            {
                while (dr.Read())
                {
                    PaperByManualSelection paper = new PaperByManualSelection();
                    paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    paper.PaperName = dr["paperName"].ToString();
                    paper.PaperType.DeptName = dr["paperType"].ToString();
                    paper.Creator.Name = dr["creator"].ToString();
                    paper.CreatedTime = Convert.ToDateTime(dr["createdTime"]);

                    list.Add(paper);
                }
            }

            return list;
        }

        public List<PaperByManualSelection> SelectList(int deptID)
        {
            List<PaperByManualSelection> list = new List<PaperByManualSelection>();
            string sql = "select * from T_PaperByManualSelection where deptID=@deptID order by paperID desc";
            SqlParameter[] parms ={
                                     new SqlParameter("@deptID",deptID)};

            using (SqlDataReader dr = DBHelper2.Select(sql, parms))
            {
                while (dr.Read())
                {
                    PaperByManualSelection paper = new PaperByManualSelection();
                    paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    paper.PaperName = dr["paperName"].ToString();
                    paper.PaperType.DeptName = dr["paperType"].ToString();
                    paper.Creator.Name = dr["creator"].ToString();
                    paper.CreatedTime = Convert.ToDateTime(dr["createdTime"]);

                    list.Add(paper);
                }
            }

            return list;
        }
        #endregion
    }
}
