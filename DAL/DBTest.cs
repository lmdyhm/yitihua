//  @ Project : TestOnline
//  @ File Name : DBTest.cs
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
    public class DBTest : DBOperation<Test>
    {
        public List<Test> SelectListByUserID(string userID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userID", SqlDbType.VarChar,30)};
            parameters[0].Value = userID;

            List<Test> testList =new List<Test>();
            using (SqlDataReader dr = DBHelper.Select("UP_T_Test_SelectListByUserID", parameters))
            {
                while (dr.Read())
                {
                    Test test = new Test();
                    test.UnabaleDate = Convert.ToDateTime(dr["unableDate"]);
                    if (DateTime.Compare(test.UnabaleDate.Date, DateTime.Now.Date) < 0)
                        break;

                    test.TestID = Convert.ToInt32(dr["testID"]);
                    test.RecorderID = Convert.ToInt32(dr["recorderID"]);
                    test.TestName = dr["testName"].ToString();
                    test.Paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    int paperType = Convert.ToInt32(dr["paperType"]);
                    switch (paperType)
                    {
                        case 1:
                            test.PaperType = PaperType.BySelection;
                            break;
                        case 2:
                            test.PaperType = PaperType.ByRandom;
                            break;
                        default:
                            test.PaperType = PaperType.ByInput;
                            break;
                    }
                    test.TotalScores = Convert.ToInt32(dr["totalScores"]);
                    test.PassScores = Convert.ToInt32(dr["passScores"]);
                    test.NeededMinutes = Convert.ToInt16(dr["neededMinutes"]);
                    test.EnableDate = Convert.ToDateTime(dr["enableDate"]);
                    test.UnabaleDate = Convert.ToDateTime(dr["unableDate"]);
                    test.AutoSaveInterval = Convert.ToInt16(dr["autoSaveInterval"]);
                    test.Creator.UserID = dr["creatorUserID"].ToString();
                    test.Creator.Name = dr["creatorName"].ToString();
                    test.CreatedTime = Convert.ToDateTime(dr["createdTime"]);

                    test.HasTested = Convert.ToBoolean(dr["hasTested"]);
                    test.Marked = Convert.ToBoolean(dr["marked"]);

                    testList.Add(test);
                }
            }
            return testList;
        }
        public void Insert(Test obj)
        {
            //------------插入test
            SqlParameter[] parameters = {
					new SqlParameter("@testID", SqlDbType.Int,4),
					new SqlParameter("@testName", SqlDbType.NVarChar,50),
					new SqlParameter("@paperID", SqlDbType.Int,4),
					new SqlParameter("@TotalScores", SqlDbType.Int,4),
					new SqlParameter("@neededMinutes", SqlDbType.TinyInt,1),
					new SqlParameter("@enableDate", SqlDbType.SmallDateTime),
					new SqlParameter("@unableDate", SqlDbType.SmallDateTime),
					new SqlParameter("@autoSaveInterval", SqlDbType.TinyInt,1),
					new SqlParameter("@creatorUserID", SqlDbType.VarChar,30),
					new SqlParameter("@creatorName", SqlDbType.NChar,10),
					new SqlParameter("@createdTime", SqlDbType.SmallDateTime),
                    new SqlParameter("@paperType",SqlDbType.Int),
                    new SqlParameter("@passScores",SqlDbType.Int,4)
                    };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = obj.TestName;
            parameters[2].Value = obj.Paper.PaperID;
            parameters[3].Value = obj.TotalScores;
            parameters[4].Value = obj.NeededMinutes;
            parameters[5].Value = obj.EnableDate;
            parameters[6].Value = obj.UnabaleDate;
            parameters[7].Value = obj.AutoSaveInterval;
            parameters[8].Value = obj.Creator.UserID;
            parameters[9].Value = obj.Creator.Name;
            parameters[10].Value = obj.CreatedTime;
            parameters[11].Value = (int)obj.PaperType;
            parameters[12].Value = obj.PassScores;
            
            SqlConnection conn = new SqlConnection(DBHelper.connStr);
            SqlTransaction trans = null;

            try
            {
                conn.Open();
                trans = conn.BeginTransaction("insertTest");
                //------------插入test
                DBHelper.Insert(trans, "UP_T_Test_ADD", parameters);
                obj.TestID =Convert.ToInt32( parameters[0].Value);

                //-------------插入testRecorder
                foreach (Tester tester in obj.TesterList)
                {
                    SqlParameter[] parameters2 = {
					new SqlParameter("@testID", SqlDbType.Int,4),
					new SqlParameter("@userID", SqlDbType.VarChar,30),
					new SqlParameter("@beginTestTime", SqlDbType.SmallDateTime),
					new SqlParameter("@submitTestTime", SqlDbType.SmallDateTime),
					new SqlParameter("@hasUsedMinutes", SqlDbType.SmallInt,2),
					new SqlParameter("@submitType", SqlDbType.NVarChar,10),
					new SqlParameter("@testerAnswer", SqlDbType.Text),
					new SqlParameter("@marked", SqlDbType.Bit,1),
                    };

                    parameters2[0].Value = obj.TestID;
                    parameters2[1].Value = tester.UserID;
                    parameters2[2].Value = null;
                    parameters2[3].Value = null;
                    parameters2[4].Value = 0;
                    parameters2[5].Value = "未提交";
                    parameters2[6].Value = string.Empty;
                    parameters2[7].Value = false ;

                    DBHelper.Insert(trans, "UP_T_TestRecorder_ADD", parameters2);
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
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public void Insert(Test test,
             int fillBlankScoresOfEveryone,
             int judgeScoresOfEveryone,
             int singleSelectionScoresOfEveryone,
             int multiSelectionScoresOfEveryone,
             List<SubjectOfSimpleAnswer> simpleAnswerScoresList)
        {

            Insert(test);

            string sql = "insert into T_Test_Subject_Scores(testID,subjectType,scores) values(@testID,@subjectType,@scores)";
            SqlParameter[] parms ={
                                     new SqlParameter("@testID",test.TestID),
                                     new SqlParameter("@subjectType",null),
                                     new SqlParameter("@scores",null)};

            SqlConnection conn = new SqlConnection(DBHelper2.connStr);
            try
            {
                conn.Open();
                //填空题
                parms[1].Value = (int)SubjectType.FillBlank;
                parms[2].Value = fillBlankScoresOfEveryone;
                DBHelper2.Insert(conn, sql, parms);

                //判断题
                parms[1].Value = (int)SubjectType.Judge;
                parms[2].Value = judgeScoresOfEveryone;
                DBHelper2.Insert(conn, sql, parms);

                //单选题
                parms[1].Value = (int)SubjectType.SingleSelection;
                parms[2].Value = singleSelectionScoresOfEveryone;
                DBHelper2.Insert(conn, sql, parms);

                //多选题
                parms[1].Value = (int)SubjectType.MultiSelection;
                parms[2].Value = multiSelectionScoresOfEveryone;
                DBHelper2.Insert(conn, sql, parms);

                //简答题
                string sql2 = "insert into T_Test_SubjectOfSimpleAnswer_Scores(testID,subjectID,scores) values(@testID,@subjectID,@scores)";
                SqlParameter[] parms2 ={
                                          new SqlParameter("@testID",test.TestID),
                                          new SqlParameter("@subjectID",null),
                                          new SqlParameter("@scores",null)};
                if(simpleAnswerScoresList!=null)
                foreach (SubjectOfSimpleAnswer subject in simpleAnswerScoresList)
                {
                    parms2[1].Value = subject.Id;
                    parms2[2].Value = subject.Scores;
                    DBHelper2.Insert(conn, sql2, parms2);
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
        }

        public void Update(Test obj)
        {
            //更新test
            SqlParameter[] parameters = {
					new SqlParameter("@testID", SqlDbType.Int,4),
					new SqlParameter("@testName", SqlDbType.NVarChar,50),
					new SqlParameter("@neededMinutes", SqlDbType.TinyInt,1),
					new SqlParameter("@enableDate", SqlDbType.SmallDateTime),
					new SqlParameter("@unableDate", SqlDbType.SmallDateTime),
					new SqlParameter("@autoSaveInterval", SqlDbType.TinyInt,1),
					new SqlParameter("@createdTime", SqlDbType.SmallDateTime)};
            parameters[0].Value = obj.TestID;
            parameters[1].Value = obj.TestName;
            parameters[2].Value = obj.NeededMinutes;
            parameters[3].Value = obj.EnableDate;
            parameters[4].Value = obj.UnabaleDate;
            parameters[5].Value = obj.AutoSaveInterval;
            parameters[6].Value = obj.CreatedTime;

            SqlConnection conn = new SqlConnection(DBHelper.connStr);
            SqlTransaction trans = null;

            try
            {
                conn.Open();
                trans = conn.BeginTransaction("updateTest");

                DBHelper.Update(trans, "UP_T_Test_Update", parameters);
                SqlParameter[] parmTemp ={
                                            new SqlParameter("@testID",obj.TestID)};
                DBHelper.Delete(trans, "UP_T_TestRecorder_DeleteByTestID", parmTemp);

                //-------------插入testRecorder
                foreach (Tester tester in obj.TesterList)
                {
                    SqlParameter[] parameters2 = {
					new SqlParameter("@testID", SqlDbType.Int,4),
					new SqlParameter("@userID", SqlDbType.VarChar,30),
					new SqlParameter("@beginTestTime", SqlDbType.SmallDateTime),
					new SqlParameter("@submitTestTime", SqlDbType.SmallDateTime),
					new SqlParameter("@hasUsedMinutes", SqlDbType.SmallInt,2),
					new SqlParameter("@submitType", SqlDbType.NVarChar,10),
					new SqlParameter("@testerAnswer", SqlDbType.Text),
					new SqlParameter("@marked", SqlDbType.Bit,1),
                    };

                    parameters2[0].Value = obj.TestID;
                    parameters2[1].Value = tester.UserID;
                    parameters2[2].Value = null;
                    parameters2[3].Value = null;
                    parameters2[4].Value = 0;
                    parameters2[5].Value = "未提交";
                    parameters2[6].Value = string.Empty;
                    parameters2[7].Value = false;

                    DBHelper.Insert(trans, "UP_T_TestRecorder_ADD", parameters2);
                }

                trans.Commit();
            }
            catch
            {
                trans.Rollback("updateTest");
            }
            finally
            {
                trans.Dispose();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public void Delete(string testID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@testID", SqlDbType.Int,4)};
            parameters[0].Value = testID;

            DBHelper.Delete("UP_T_Test_Delete", parameters);
        }
        public Test SelectByID(string testID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@testID", SqlDbType.Int,4)};
            parameters[0].Value = testID;

            Test test = null;
            using (SqlDataReader dr = DBHelper.Select("UP_T_Test_GetModel", parameters))
            {
                if (dr.Read())
                {
                    test = new Test();
                    test.TestID = Convert.ToInt32(dr["testID"]);
                    test.TestName = dr["testName"].ToString();
                    test.Paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    int paperType = Convert.ToInt32(dr["paperType"]);
                    switch (paperType)
                    {
                        case 1:
                            test.PaperType = PaperType.BySelection;
                            break;
                        case 2:
                            test.PaperType = PaperType.ByRandom;
                            break;
                        default:
                            test.PaperType = PaperType.ByInput;
                            break;
                    }
                    test.TotalScores = Convert.ToInt32(dr["totalScores"]);
                    test.PassScores = Convert.ToInt32(dr["passScores"]);
                    test.NeededMinutes = Convert.ToInt16(dr["neededMinutes"]);
                    test.EnableDate = Convert.ToDateTime(dr["enableDate"]);
                    test.UnabaleDate = Convert.ToDateTime(dr["unableDate"]);
                    test.AutoSaveInterval = Convert.ToInt16(dr["autoSaveInterval"]);
                    test.Creator.UserID = dr["creatorUserID"].ToString();
                    test.Creator.Name = dr["creatorName"].ToString();
                    test.CreatedTime = Convert.ToDateTime(dr["createdTime"]);

                }
            }

            return test;
        }
        public List<Test> SelectList()
        {
            List<Test> testList = new List<Test>();
            using (SqlDataReader dr = DBHelper.Select("UP_T_Test_GetList", null))
            {
                while (dr.Read())
                {
                    Test test = new Test();
                    test.TestID = Convert.ToInt32(dr["testID"]);
                    test.TestName = dr["testName"].ToString();
                    test.Paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    int paperType = Convert.ToInt32(dr["paperType"]);
                    switch (paperType)
                    {
                        case 1:
                            test.PaperType = PaperType.BySelection;
                            break;
                        case 2:
                            test.PaperType = PaperType.ByRandom;
                            break;
                        default:
                            test.PaperType = PaperType.ByInput;
                            break;
                    }
                    test.TotalScores = Convert.ToInt32(dr["totalScores"]);
                    test.PassScores = Convert.ToInt32(dr["passScores"]);
                    test.NeededMinutes = Convert.ToInt16(dr["neededMinutes"]);
                    test.EnableDate = Convert.ToDateTime(dr["enableDate"]);
                    test.UnabaleDate = Convert.ToDateTime(dr["unableDate"]);
                    test.AutoSaveInterval =Convert.ToInt16(dr["autoSaveInterval"]);
                    test.Creator.UserID = dr["creatorUserID"].ToString();
                    test.Creator.Name = dr["creatorName"].ToString();
                    test.CreatedTime = Convert.ToDateTime(dr["createdTime"]);

                    testList.Add(test);
                }
            }
            return testList;
        }

        public List<Test> SelectListForMarks()
        {
            List<Test> testList = new List<Test>();
            using (SqlDataReader dr = DBHelper.Select("UP_T_Test_GetListForMarks", null))
            {
                while (dr.Read())
                {
                    Test test = new Test();
                    test.TestID = Convert.ToInt32(dr["testID"]);
                    test.TestName = dr["testName"].ToString();
                    test.PaperType =(PaperType)dr["paperType"];
                    test.TotalScores = Convert.ToInt32(dr["totalScores"]);
                    test.PassScores = Convert.ToInt32(dr["passScores"]);
                    test.TesterCount = Convert.ToInt32(dr["testerCount"]);
                    test.TesterCountHasTested = Convert.ToInt32(dr["testerCountHasTested"]);

                    testList.Add(test);
                }
            }
            return testList;
        }
    }
}