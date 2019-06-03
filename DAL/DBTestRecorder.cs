//  @ Project : TestOnline
//  @ File Name : DBTestRecorder.cs
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
    public class DBTestRecorder : DBOperation<TestRecorder>
    {
        public List<TestRecorder> SelectListByTestID(int testID)
        {
            List<TestRecorder> recorderList = new List<TestRecorder>();

            SqlParameter[] parms ={
                new SqlParameter("@testID",SqlDbType.Int,4)};
            parms[0].Value = testID;

            using (SqlDataReader dr = DBHelper.Select("UP_T_TestRecorder_GetListByTestID", parms))
            {
                while (dr.Read())
                {
                    TestRecorder recoder = new TestRecorder();
                    recoder.Test.TestID = testID;
                    recoder.Test.TestName = dr["testName"].ToString();
                    recoder.Test.PaperType = (PaperType)dr["paperType"];
                    recoder.Test.Paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    Tester tester = new Tester();
                    recoder.RecorderID = Convert.ToInt32(dr["recorderID"]);
                    tester.UserID = dr["userID"].ToString();
                    tester.Name = dr["Name"].ToString();
                    Department dept = new Department();
                    dept.DeptName = dr["deptName"].ToString();
                    tester.Department = dept;
                    recoder.Tester = tester;
                    if (!string.IsNullOrEmpty(dr["BeginTestTime"].ToString()))
                        recoder.BeginTestTime = Convert.ToDateTime(dr["BeginTestTime"]);
                    else
                        recoder.BeginTestTime = new DateTime();

                    if (!string.IsNullOrEmpty(dr["SubmitTestTime"].ToString()))
                        recoder.SubmitTestTime = Convert.ToDateTime(dr["SubmitTestTime"]);
                    else
                        recoder.SubmitTestTime = new DateTime();

                    recoder.SubmitType = dr["SubmitType"].ToString();
                    recoder.Marked = Convert.ToBoolean(dr["marked"]);
                    recoder.HasTested = Convert.ToBoolean(dr["hasTested"]);

                    if (!dr["totalScore"].ToString().Trim().Equals(""))
                        recoder.TotalScore = Convert.ToInt32(dr["totalScore"]);
                    //     recoder.TestMark.TotalScore = Convert.ToUInt16(dr["totalScore"]);
                    //recoder.TestMark.Remark = dr["remark"].ToString();

                    recorderList.Add(recoder);
                }
            }

            return recorderList;
        }

        public List<TestRecorder> SelectListByUserID(string userID)
        {
            List<TestRecorder> recorderList = new List<TestRecorder>();

            SqlParameter[] parms ={
                new SqlParameter("@userID",SqlDbType.VarChar,30)};
            parms[0].Value = userID;

            using (SqlDataReader dr = DBHelper.Select("UP_T_TestRecorder_GetListByUserID", parms))
            {
                while (dr.Read())
                {
                    TestRecorder recoder = new TestRecorder();
                    recoder.Test.TestID = Convert.ToInt32(dr["TestID"]);
                    recoder.Test.TestName = dr["testName"].ToString();
                    recoder.Test.NeededMinutes = Convert.ToInt16(dr["neededMinutes"]);
                    Tester tester = new Tester();
                    recoder.RecorderID = Convert.ToInt32(dr["recorderID"]);
                    tester.UserID = dr["userID"].ToString();
                    recoder.Tester = tester;
                    if (!string.IsNullOrEmpty(dr["BeginTestTime"].ToString()))
                        recoder.BeginTestTime = Convert.ToDateTime(dr["BeginTestTime"]);
                    else
                        recoder.BeginTestTime = new DateTime();

                    if (!string.IsNullOrEmpty(dr["SubmitTestTime"].ToString()))
                        recoder.SubmitTestTime = Convert.ToDateTime(dr["SubmitTestTime"]);
                    else
                        recoder.SubmitTestTime = new DateTime();

                    recoder.SubmitType = dr["SubmitType"].ToString();
                    recoder.HasTested = Convert.ToBoolean(dr["hasTested"]);
                    recorderList.Add(recoder);
                }
            }

            return recorderList;
        }


        public TestRecorder SelectBy(int testID, string userID)
        {
            TestRecorder recorder = null;
            SqlParameter[] parms ={
                new SqlParameter("@testID",SqlDbType.Int,4),
                new SqlParameter("@userID",SqlDbType.VarChar,30)};
            parms[0].Value = testID;
            parms[1].Value = userID;

            using (SqlDataReader dr = DBHelper.Select("UP_T_TestRecorder_GetModel", parms))
            {
                if (dr.Read())
                {
                    recorder = new TestRecorder();
                    recorder.RecorderID = Convert.ToInt32(dr["recorderID"]);
                    recorder.Test.TestID = testID;
                    recorder.Test.EnableDate = Convert.ToDateTime(dr["enableDate"]);
                    recorder.Test.UnabaleDate = Convert.ToDateTime(dr["unableDate"]);
                    recorder.Tester.UserID = userID;
                    recorder.HasTested = Convert.ToBoolean(dr["hasTested"]);
                }
            }
            return recorder;

        }
        public void Update(int testID,string userID, string testerAnswer)
        {
            SqlParameter[] parms ={
                new SqlParameter("@testID",SqlDbType.Int,4),
                new SqlParameter("@userID",SqlDbType.VarChar,30),
                new SqlParameter("@testerAnswer",SqlDbType.Text),
                new SqlParameter("@submitTestTime",SqlDbType.DateTime)};
            parms[0].Value = testID;
            parms[1].Value = userID;
            parms[2].Value = testerAnswer;
            parms[3].Value = DateTime.Now;

           int result= DBHelper.Update("UP_T_TestRecorder_UpdateAnswer", parms);

        }
        /// <summary>
        /// 更新：hasTested=true,beginTestTime=getdate()
        /// </summary>
        public void Update_BeginTest(int testID,string userID,DateTime beginTestTime)
        {
            SqlParameter[] parms ={
                new SqlParameter("@testID",SqlDbType.Int,4),
                new SqlParameter("@beginTestTime",SqlDbType.SmallDateTime),
                new SqlParameter("@userID",SqlDbType.VarChar,30)};
            parms[0].Value = testID;
            parms[1].Value = beginTestTime;
            parms[2].Value = userID;

            DBHelper.Update("UP_T_TestRecorder_Update_BeginTest", parms);
        }
        public void Update(int testID,string userID, string testerAnswer, string submitType, DateTime submitTestTime)
        {
            SqlParameter[] parms ={new SqlParameter("@testID",SqlDbType.Int,4),
                    new SqlParameter("@testerAnswer",SqlDbType.Text),
                    new SqlParameter("@submitType",SqlDbType.NVarChar,10),
                    new SqlParameter("@submitTestTime",SqlDbType.SmallDateTime),
                    new SqlParameter("@userID",SqlDbType.VarChar,30)};
            parms[0].Value = testID;
            parms[1].Value = testerAnswer;
            parms[2].Value = submitType;
            parms[3].Value = submitTestTime;
            parms[4].Value = userID;

            DBHelper.Update("UP_T_TestRecorder_UpdateAfterSubmit", parms);
        }
        public void Insert(TestRecorder obj)
        {
        }
        public void Update(TestRecorder obj)
        {
        }
        public void Delete(string id)
        {
        }
        public TestRecorder SelectByID(string recorderID)
        {
            TestRecorder recoder = null;

            SqlParameter[] parms ={
                new SqlParameter("@recorderID",SqlDbType.Int,4)};
            parms[0].Value = recorderID;

            using (SqlDataReader dr = DBHelper.Select("UP_T_TestRecorder_GetListForMarking", parms))
            {
                if (dr.Read())
                {
                    recoder = new TestRecorder();
                    Tester tester = new Tester();
                    recoder.RecorderID = Convert.ToInt32(dr["recorderID"]);
                    tester.UserID = dr["userID"].ToString();
                    tester.Name = dr["Name"].ToString();
                    recoder.Tester = tester;
                    recoder.TesterAnswer = dr["testerAnswer"].ToString();
                    Paper paper = new Paper();
                    paper.PaperID = Convert.ToInt32(dr["paperID"]);
                    paper.Answer = dr["Answer"].ToString();
                    recoder.Test.Paper = paper;
                    recoder.Test.TestID = Convert.ToInt32(dr["testID"]);
                    recoder.Test.TestName = dr["testName"].ToString();
                    recoder.Marked = Convert.ToBoolean(dr["marked"]);
                    if (!dr["totalScore"].ToString().Trim().Equals(""))
                        recoder.TotalScore = Convert.ToInt32(dr["totalScore"]);
                    
                }
            }
            return recoder;
            
        }
        public List<TestRecorder> SelectList()
        {
            return null;
        }

        public void InsertTestRecorderAnswer(int recorderID)
        {
            SqlParameter[] parms ={
                                     new SqlParameter("@recorderID",recorderID)};

            DBHelper.Insert("UP_T_TestRecorder_Answer_ADD", parms);
        }

        public void InsertTestRecorderAnswer(int recorderID, List<SubjectOfFillBlank> list1, List<SubjectOfJudge> list2, List<SubjectOfSingleSelection> list3, List<SubjectOfMultiSelection> list4, List<SubjectOfSimpleAnswer> list5)
        {
            SqlConnection conn = new SqlConnection(DBHelper2.connStr);
            try
            {
                conn.Open();
                string sql = "insert into T_TestRecorder_Answer(recorderID,subjectType,subjectID) values(@recorderID,@subjectType,@subjectID)";
                SqlParameter[] parms ={
                                          new SqlParameter("@recorderID",recorderID),
                                          new SqlParameter("@subjectType",0),
                                           new SqlParameter("@subjectID",0)};
                //填空题
                if (list1 != null)
                    foreach (SubjectOfFillBlank subject in list1)
                    {
                        parms[1].Value = (int)SubjectType.FillBlank;
                        parms[2].Value = subject.Id;
                        DBHelper2.Insert(sql, parms);
                    }

                //判断题
                if (list2 != null)
                    foreach (SubjectOfJudge subject in list2)
                    {
                        parms[1].Value = (int)SubjectType.Judge;
                        parms[2].Value = subject.Id;
                        DBHelper2.Insert(sql, parms);
                    }

                //单选题
                if (list3 != null)
                    foreach (SubjectOfSingleSelection subject in list3)
                    {
                        parms[1].Value = (int)SubjectType.SingleSelection;
                        parms[2].Value = subject.Id;
                        DBHelper2.Insert(sql, parms);
                    }

                //多选题
                if (list4 != null)
                    foreach (SubjectOfMultiSelection subject in list4)
                    {
                        parms[1].Value = (int)SubjectType.MultiSelection;
                        parms[2].Value = subject.Id;
                        DBHelper2.Insert(sql, parms);
                    }

                // 简答题
                if (list5 != null)
                    foreach (SubjectOfSimpleAnswer subject in list5)
                    {
                        parms[1].Value = (int)SubjectType.SimpleAnswer;
                        parms[2].Value = subject.Id;
                        DBHelper2.Insert(sql, parms);
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateTestRecorderAnswer(int recorderID, List<SubjectOfFillBlank> list1, List<SubjectOfJudge> list2, List<SubjectOfSingleSelection> list3, List<SubjectOfMultiSelection> list4, List<SubjectOfSimpleAnswer> list5)
        {
            SqlConnection conn = new SqlConnection(DBHelper2.connStr);
            try
            {
                conn.Open();

                //填空题
                if(list1!=null)
                foreach (SubjectOfFillBlank subject in list1)
                {
                    string sql = "update T_TestRecorder_Answer set answer1=@answer1 where recorderID=@recorderID and subjectType=1 and subjectID=@subjectID";
                    SqlParameter[] parms ={
                                          new SqlParameter("@answer1",subject.Answer),
                                          new SqlParameter("@recorderID",recorderID),
                                          new SqlParameter("@subjectID",subject.Id)};
                    DBHelper2.Update(sql, parms);
                }

                //判断题
                if(list2!=null)
                foreach (SubjectOfJudge subject in list2)
                {
                    string sql = "update T_TestRecorder_Answer set answer2=@answer2 where recorderID=@recorderID and subjectType=2 and subjectID=@subjectID";
                    SqlParameter[] parms ={
                                          new SqlParameter("@answer2",subject.Answer),
                                          new SqlParameter("@recorderID",recorderID),
                                          new SqlParameter("@subjectID",subject.Id)};
                    DBHelper2.Update(sql, parms);
                }

                //单选题
                if(list3!=null)
                foreach (SubjectOfSingleSelection subject in list3)
                {
                    string sql = "update T_TestRecorder_Answer set answer3=@answer3 where recorderID=@recorderID and subjectType=3 and subjectID=@subjectID";
                    SqlParameter[] parms ={
                                          new SqlParameter("@answer3",subject.Answer),
                                          new SqlParameter("@recorderID",recorderID),
                                          new SqlParameter("@subjectID",subject.Id)};
                    DBHelper2.Update(sql, parms);
                }

                //多选题
                if(list4!=null)
                foreach (SubjectOfMultiSelection subject in list4)
                {
                    string sql = "update T_TestRecorder_Answer set answer4=@answer4 where recorderID=@recorderID and subjectType=4 and subjectID=@subjectID";
                    SqlParameter[] parms ={
                                          new SqlParameter("@answer4",subject.Answer),
                                          new SqlParameter("@recorderID",recorderID),
                                          new SqlParameter("@subjectID",subject.Id)};
                    DBHelper2.Update(sql, parms);
                }

                //简答题
                if(list5!=null)
                foreach (SubjectOfSimpleAnswer subject in list5)
                {
                    string sql = "update T_TestRecorder_Answer set answer5=@answer5 where recorderID=@recorderID and subjectType=5 and subjectID=@subjectID";
                    SqlParameter[] parms ={
                                          new SqlParameter("@answer5",subject.Answer),
                                          new SqlParameter("@recorderID",recorderID),
                                          new SqlParameter("@subjectID",subject.Id)};
                    DBHelper2.Update(sql, parms);
                }

                //更新分数
                SqlParameter[] parm ={
                                         new SqlParameter("@recorderID",recorderID)};
                DBHelper.Update(conn,"UP_TestRecorder_Answer_UpdateScores", parm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //评卷：保存成绩
        public void UpdateTestRecorderAnswer_Scores(int recorderID, List<SubjectOfFillBlank> list1, List<SubjectOfJudge> list2, List<SubjectOfSingleSelection> list3, List<SubjectOfMultiSelection> list4, List<SubjectOfSimpleAnswer> list5)
        {
            SqlConnection conn = new SqlConnection(DBHelper2.connStr);
            try
            {
                conn.Open();
                string sql = "update T_TestRecorder_Answer set scores=@scores where recorderID=@recorderID and subjectType=@subjectType and subjectID=@subjectID";
                SqlParameter[] parms ={
                                          new SqlParameter("@recorderID",SqlDbType.Int),
                                          new SqlParameter("@scores",SqlDbType.Int),
                                          new SqlParameter("@subjectType",SqlDbType.Int),
                                          new SqlParameter("@subjectID",SqlDbType.Int)};
                parms[0].Value = recorderID;

                //填空题
                if (list1 != null)
                    foreach (SubjectOfFillBlank subject in list1)
                    {
                        parms[1].Value = subject.ScoresForTester;
                        parms[2].Value = (int)SubjectType.FillBlank;
                        parms[3].Value = subject.Id;
                        DBHelper2.Update(sql, parms);
                    }

                //判断题
                if (list2 != null)
                    foreach (SubjectOfJudge subject in list2)
                    {
                        parms[1].Value = subject.ScoresForTester;
                        parms[2].Value = (int)SubjectType.Judge;
                        parms[3].Value = subject.Id;
                        DBHelper2.Update(sql, parms);
                    }

                //单选题
                if (list3 != null)
                    foreach (SubjectOfSingleSelection subject in list3)
                    {
                        parms[1].Value = subject.ScoresForTester;
                        parms[2].Value = (int)SubjectType.SingleSelection;
                        parms[3].Value = subject.Id;
                        DBHelper2.Update(sql, parms);
                    }

                //多选题
                if (list4 != null)
                    foreach (SubjectOfMultiSelection subject in list4)
                    {
                        parms[1].Value = subject.ScoresForTester;
                        parms[2].Value = (int)SubjectType.MultiSelection;
                        parms[3].Value = subject.Id;
                        DBHelper2.Update(sql, parms);
                    }

                //简答题
                if (list5 != null)
                    foreach (SubjectOfSimpleAnswer subject in list5)
                    {
                        parms[1].Value = subject.ScoresForTester;
                        parms[2].Value = (int)SubjectType.SimpleAnswer;
                        parms[3].Value = subject.Id;
                        DBHelper2.Update(sql, parms);
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //填充考生的回答和分数
        public void SetTestRecorderAnswer(int recorderID, List<SubjectOfFillBlank> list1, List<SubjectOfJudge> list2, List<SubjectOfSingleSelection> list3, List<SubjectOfMultiSelection> list4, List<SubjectOfSimpleAnswer> list5)
        {
            string sql = "select * from T_TestRecorder_Answer where recorderID=@recorderID";
            SqlParameter[] parms ={
                                     new SqlParameter("@recorderID",recorderID)};
            using (SqlDataReader dr = DBHelper2.Select(sql, parms))
            {
                while (dr.Read())
                {
                    int subjectType = Convert.ToInt32(dr["subjectType"]);
                    int subjectID = Convert.ToInt32(dr["subjectID"]);
                    switch (subjectType)
                    {
                        case 1://填空题
                            if(list1!=null)
                                foreach (SubjectOfFillBlank subject in list1)
                                {
                                    if (subject.Id == subjectID)
                                    {
                                        subject.AnswerByTester = dr["answer1"].ToString();
                                        subject.ScoresForTester = Convert.ToInt32(dr["scores"]);
                                        break;
                                    }
                                }
                            break;
                        case 2://判断题
                            if (list2 != null)
                                foreach (SubjectOfJudge subject in list2)
                                {
                                    if (subject.Id == subjectID)
                                    {
                                        try
                                        {
                                            subject.AnswerByTester = Convert.ToBoolean(dr["answer2"]);
                                        }
                                        catch { }
                                        subject.ScoresForTester = Convert.ToInt32(dr["scores"]);
                                        if (subject.ScoresForTester == 0 && subject.AnswerByTester==subject.Answer)
                                        {
                                            subject.ScoresForTester = subject.Scores;
                                        }
                                        break;
                                    }
                                }
                            break;
                        case 3://单选题
                            if (list3 != null)
                                foreach (SubjectOfSingleSelection subject in list3)
                                {
                                    if (subject.Id == subjectID)
                                    {
                                        try
                                        {
                                            subject.AnswerByTester = Convert.ToChar(dr["answer3"]);
                                        }
                                        catch { }
                                        subject.ScoresForTester = Convert.ToInt32(dr["scores"]);
                                        if (subject.ScoresForTester == 0 && subject.AnswerByTester == subject.Answer)
                                        {
                                            subject.ScoresForTester = subject.Scores;
                                        }
                                        break;
                                    }
                                }
                            break;
                        case 4://多选题
                            if (list4 != null)
                                foreach (SubjectOfMultiSelection subject in list4)
                                {
                                    if (subject.Id == subjectID)
                                    {
                                        subject.AnswerByTester = dr["answer4"].ToString();
                                        subject.ScoresForTester = Convert.ToInt32(dr["scores"]);
                                        if (subject.ScoresForTester == 0 && subject.AnswerByTester.Equals(subject.Answer))
                                        {
                                            subject.ScoresForTester = subject.Scores;
                                        }
                                        break;
                                    }
                                }
                            break;
                        case 5://简答题
                            if (list5 != null)
                                foreach (SubjectOfSimpleAnswer subject in list5)
                                {
                                    if (subject.Id == subjectID)
                                    {
                                        subject.AnswerByTester = dr["answer5"].ToString();
                                        subject.ScoresForTester = Convert.ToInt32(dr["scores"]);
                                        break;
                                    }
                                }
                            break;
                    }
                }
            }
        }
    }

}
