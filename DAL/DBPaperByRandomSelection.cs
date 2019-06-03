using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;

namespace DAL
{
    public class DBPaperByRandomSelection : DBOperation<PaperByRandomSelection>
    {
        #region DBOperation<PaperByRandomSelection> Members

        public void Insert(PaperByRandomSelection obj)
        {
            string sql = "insert into T_PaperByRandomSelection(paperName,deptID,paperType,creator,createdTime,judgeSum,singleSelectionSum,multiSelectionSum,judgeCateID,singleSelectionCateID,multiSelectionCateID) values(@paperName,@deptID,@paperType,@creator,@createdTime,@judgeSum,@singleSelectionSum,@multiSelectionSum,@judgeCateID,@singleSelectionCateID,@multiSelectionCateID)";
            SqlParameter[] parms ={
                                     new SqlParameter("@paperName",obj.PaperName),
                                     new SqlParameter("@deptID",obj.PaperType.DeptID),
                                     new SqlParameter("@paperType",obj.PaperType.DeptName),
                                     new SqlParameter("@creator",obj.Creator.Name),
                                     new SqlParameter("@createdTime",obj.CreatedTime),
                                     new SqlParameter("@judgeSum",obj.JudgeSum),
                                     new SqlParameter("@singleSelectionSum",obj.SingleSelectionSum),
                                     new SqlParameter("@multiSelectionSum",obj.MultiSelectionSum),
                                     new SqlParameter("@judgeCateID",obj.JudgeCateID),
                                     new SqlParameter("@singleSelectionCateID",obj.SingleSelectionCateID),
                                     new SqlParameter("@multiSelectionCateID",obj.MultiSelectionCateID)};
            DBHelper2.Insert(sql, parms);
        }

        public void Update(PaperByRandomSelection obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            string sql = "delete from T_PaperByRandomSelection where paperID=@paperID";
            SqlParameter[] parms ={
                                     new SqlParameter("@paperID",id)};
            DBHelper2.Delete(sql, parms);
        }

        /// <summary>
        /// 只获取试卷的基本信息
        /// </summary>
        public PaperByRandomSelection SelectByID2(string id)
        {
            string sql = "select * from T_PaperByRandomSelection where paperID=@paperID";
            SqlParameter[] parms ={
                                     new SqlParameter("@paperID",id)};
            PaperByRandomSelection paper =null;
            using (SqlDataReader dr = DBHelper2.Select(sql, parms))
            {
                if (dr.Read())
                {
                    paper = new PaperByRandomSelection();
                    paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    paper.PaperName = dr["paperName"].ToString();
                    paper.PaperType.DeptName = dr["paperType"].ToString();
                    paper.Creator.Name = dr["creator"].ToString();
                    paper.CreatedTime = Convert.ToDateTime(dr["createdTime"]);
                    paper.JudgeSum = Convert.ToInt32(dr["judgeSum"]);
                    paper.SingleSelectionSum = Convert.ToInt32(dr["singleSelectionSum"]);
                    paper.MultiSelectionSum = Convert.ToInt32(dr["multiSelectionSum"]);
                }
            }
            return paper;
        }

        /// <summary>
        /// 除了试卷的基本信息外，也包含各类题库集合(随机生成)
        /// </summary>
        public PaperByRandomSelection SelectByID(string id)
        {
            SqlConnection conn=new SqlConnection(DBHelper2.connStr);
            conn.Open();

            string sql = "select * from T_PaperByRandomSelection where paperID=@paperID";
            SqlParameter[] parms ={
                                     new SqlParameter("@paperID",id)};
            PaperByRandomSelection paper =null;
            using (SqlDataReader dr = DBHelper2.Select(conn, sql, parms))
            {
                if (dr.Read())
                {
                    paper = new PaperByRandomSelection();
                    paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    paper.PaperName = dr["paperName"].ToString();
                    paper.PaperType.DeptName = dr["paperType"].ToString();
                    paper.Creator.Name = dr["creator"].ToString();
                    paper.CreatedTime = Convert.ToDateTime(dr["createdTime"]);
                    paper.JudgeSum = Convert.ToInt32(dr["judgeSum"]);
                    paper.SingleSelectionSum = Convert.ToInt32(dr["singleSelectionSum"]);
                    paper.MultiSelectionSum = Convert.ToInt32(dr["multiSelectionSum"]);
                    paper.JudgeCateID = Convert.ToInt32(dr["judgeCateID"]);
                    paper.SingleSelectionCateID = Convert.ToInt32(dr["singleSelectionCateID"]);
                    paper.MultiSelectionCateID = Convert.ToInt32(dr["multiSelectionCateID"]);
                    dr.Close();

                    //判断题
                    sql = "select top " + paper.JudgeSum + " * from T_SubjectOfJudge where cateID=@judgeCateID order by newid()";
                    SqlParameter[] parm1 = { new SqlParameter("@judgeCateID", paper.JudgeCateID) };
                    paper.JudgeList = new List<SubjectOfJudge>();
                    using (SqlDataReader dr2 = DBHelper2.Select(conn, sql, parm1))
                    {
                        while (dr2.Read())
                        {
                            SubjectOfJudge subject = new SubjectOfJudge();
                            subject.Index = paper.JudgeList.Count + 1;
                            subject.Id = Convert.ToInt32(dr2["id"]);
                            subject.Question = dr2["question"].ToString();
                            subject.Answer = Convert.ToBoolean(dr2["answer"]);
                            paper.JudgeList.Add(subject);
                        }
                    }

                    //单选题
                    sql = "select top " + paper.SingleSelectionSum + " * from T_SubjectOfSingleSelection where cateID=@singleSelectionCateID order by newid()";
                    SqlParameter[] parm2 = { new SqlParameter("@singleSelectionCateID", paper.SingleSelectionCateID) };
                    paper.SingleSelectionList = new List<SubjectOfSingleSelection>();
                    using (SqlDataReader dr2 = DBHelper2.Select(conn, sql, parm2))
                    {
                        while (dr2.Read())
                        {
                            SubjectOfSingleSelection subject = new SubjectOfSingleSelection();
                            subject.Index = paper.SingleSelectionList.Count + 1;
                            subject.Id = Convert.ToInt32(dr2["id"]);
                            subject.Question = dr2["question"].ToString();
                            subject.Answer = Convert.ToChar(dr2["answer"]);
                            subject.SelectA = dr2["selectA"].ToString();
                            subject.SelectB = dr2["selectB"].ToString();
                            subject.SelectC = dr2["selectC"].ToString();
                            subject.SelectD = dr2["selectD"].ToString();
                            paper.SingleSelectionList.Add(subject);
                        }
                    }

                    //多选题
                    sql = "select top " + paper.MultiSelectionSum + " * from T_SubjectOfMultiSelection where cateID=@multiSelectionCateID order by newid()";
                    SqlParameter[] parm3 = { new SqlParameter("@multiSelectionCateID", paper.MultiSelectionCateID) };
                    paper.MultiSelectionList = new List<SubjectOfMultiSelection>();
                    using (SqlDataReader dr2 = DBHelper2.Select(conn, sql, parm3))
                    {
                        while (dr2.Read())
                        {
                            SubjectOfMultiSelection subject = new SubjectOfMultiSelection();
                            subject.Index = paper.MultiSelectionList.Count + 1;
                            subject.Id = Convert.ToInt32(dr2["id"]);
                            subject.Question = dr2["question"].ToString();
                            subject.Answer = dr2["answer"].ToString();
                            subject.SelectA = dr2["selectA"].ToString();
                            subject.SelectB = dr2["selectB"].ToString();
                            subject.SelectC = dr2["selectC"].ToString();
                            subject.SelectD = dr2["selectD"].ToString();
                            paper.MultiSelectionList.Add(subject);
                        }
                    }

                }
            }
            return paper;
        }

        /// <summary>
        /// 除了试卷的基本信息外，也包含各类题库集合(随机生成，含各题的分数）
        /// </summary>
        public PaperByRandomSelection SelectByID(int testID, int paperID)
        {
            PaperByRandomSelection paper = SelectByID(paperID.ToString());

            SqlConnection conn = new SqlConnection(DBHelper2.connStr);

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
                        int subjectType = Convert.ToInt32(dr["subjectType"]);
                        switch ((SubjectType)subjectType)
                        {
                            case SubjectType.Judge:
                                foreach (SubjectOfJudge subject in paper.JudgeList)
                                {
                                    subject.Scores = Convert.ToInt32(dr["scores"]);
                                }
                                break;
                            case SubjectType.SingleSelection:
                                foreach (SubjectOfSingleSelection subject in paper.SingleSelectionList)
                                {
                                    subject.Scores = Convert.ToInt32(dr["scores"]);
                                }
                                break;
                            case SubjectType.MultiSelection:
                                foreach (SubjectOfMultiSelection subject in paper.MultiSelectionList)
                                {
                                    subject.Scores = Convert.ToInt32(dr["scores"]);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }

            return paper;
        }

        /// <summary>
        /// 获取考生的实际应考试卷和回答
        /// </summary>
        public PaperByRandomSelection SelectByID3(int testID, int recorderID)
        {
            SqlConnection conn = new SqlConnection(DBHelper2.connStr);
            PaperByRandomSelection paper = new PaperByRandomSelection();
            try
            {
                conn.Open();
                string sql = string.Empty;
                sql+= "SELECT s.*";
                sql+=",(SELECT scores FROM T_Test_Subject_Scores WHERE (testID = @testID) AND (subjectType = @subjectType)) AS scores";
                sql+=", t.{0}, t.scores AS scoresForTester";
                sql += " FROM T_TestRecorder_Answer AS t INNER JOIN {1} AS s ON t.subjectID=s.id ";
                sql+=" WHERE (t.recorderID = @recorderID) AND (t.subjectType = @subjectType)";

                SqlParameter[] parms ={
                                          new SqlParameter("@subjectType",0),
                                          new SqlParameter("@testID",testID),
                                         new SqlParameter("@recorderID",recorderID)};

                //判断题 
                string sql1 = string.Format(sql, "answer2","T_SubjectOfJudge");
                parms[0].Value = (int)SubjectType.Judge;
                using (SqlDataReader dr = DBHelper2.Select(conn, sql1, parms))
                {
                    paper.JudgeList = new List<SubjectOfJudge>();
                    while (dr.Read())
                    {
                        SubjectOfJudge subject = new SubjectOfJudge();
                        try
                        {
                            subject.Id = Convert.ToInt32(dr["id"]);
                            subject.Question = dr["question"].ToString();
                            subject.Answer = Convert.ToBoolean(dr["answer"]);
                            subject.Scores = Convert.ToInt32(dr["scores"]);
                            subject.AnswerByTester = Convert.ToBoolean(dr["answer2"]);
                            subject.ScoresForTester = Convert.ToInt32(dr["scoresForTester"]);
                        }
                        catch { }
                        paper.JudgeList.Add(subject);
                    }
                }

                //单选题 
                string sql2 = string.Format(sql, "answer3","T_SubjectOfSingleSelection");
                parms[0].Value = (int)SubjectType.SingleSelection;
                using (SqlDataReader dr = DBHelper2.Select(conn, sql2, parms))
                {
                    paper.SingleSelectionList = new List<SubjectOfSingleSelection>();
                    while (dr.Read())
                    {
                        SubjectOfSingleSelection subject = new SubjectOfSingleSelection();
                        try
                        {
                            subject.Id = Convert.ToInt32(dr["id"]);
                            subject.Question = dr["question"].ToString();
                            subject.SelectA = dr["selectA"].ToString();
                            subject.SelectB = dr["selectB"].ToString();
                            subject.SelectC = dr["selectC"].ToString();
                            subject.SelectD = dr["selectD"].ToString();
                            subject.Answer = Convert.ToChar(dr["answer"]);
                            subject.Scores = Convert.ToInt32(dr["scores"]);
                            subject.AnswerByTester = Convert.ToChar(dr["answer3"]);
                            subject.ScoresForTester = Convert.ToInt32(dr["scoresForTester"]);
                        }
                        catch { }
                        paper.SingleSelectionList.Add(subject);
                    }
                }

                //多选题 
                string sql3 = string.Format(sql,"answer4", "T_SubjectOfMultiSelection");
                parms[0].Value = (int)SubjectType.MultiSelection;
                using (SqlDataReader dr = DBHelper2.Select(conn, sql3, parms))
                {
                    paper.MultiSelectionList = new List<SubjectOfMultiSelection>();
                    while (dr.Read())
                    {
                         SubjectOfMultiSelection subject = new SubjectOfMultiSelection();
                         try
                         {
                             subject.Id = Convert.ToInt32(dr["id"]);
                             subject.Question = dr["question"].ToString();
                             subject.SelectA = dr["selectA"].ToString();
                             subject.SelectB = dr["selectB"].ToString();
                             subject.SelectC = dr["selectC"].ToString();
                             subject.SelectD = dr["selectD"].ToString();
                             subject.Answer = dr["answer"].ToString();
                             subject.Scores = Convert.ToInt32(dr["scores"]);
                             subject.AnswerByTester = dr["answer4"].ToString();
                             subject.ScoresForTester = Convert.ToInt32(dr["scoresForTester"]);
                         }
                         catch { }
                        paper.MultiSelectionList.Add(subject);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }

            return paper;
        }

        public List<PaperByRandomSelection> SelectList()
        {
            string sql = "select * from T_PaperByRandomSelection order by paperID desc";
            List<PaperByRandomSelection> list = new List<PaperByRandomSelection>();

            using (SqlDataReader dr = DBHelper2.Select(sql, null))
            {
                while (dr.Read())
                {
                    PaperByRandomSelection paper = new PaperByRandomSelection();
                    paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    paper.PaperName = dr["paperName"].ToString();
                    paper.PaperType.DeptName = dr["paperType"].ToString();
                    paper.Creator.Name = dr["creator"].ToString();
                    paper.CreatedTime = Convert.ToDateTime(dr["createdTime"]);
                    paper.JudgeSum = Convert.ToInt32(dr["judgeSum"]);
                    paper.SingleSelectionSum = Convert.ToInt32(dr["singleSelectionSum"]);
                    paper.MultiSelectionSum = Convert.ToInt32(dr["multiSelectionSum"]);

                    list.Add(paper);
                }
                return list;
            }
        }
        #endregion
    }
}
