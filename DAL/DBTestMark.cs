//  @ Project : TestOnline
//  @ File Name : DBTestMark.cs
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
    public class DBTestMark : DBOperation<TestMark>
    {
        public void Insert(TestMark obj)
        {
            SqlParameter[] parms ={
                new SqlParameter("@totalScore",SqlDbType.SmallInt,2),
                new SqlParameter("@remark",SqlDbType.NVarChar,1000),
                new SqlParameter("@marker",SqlDbType.NVarChar,10),
                new SqlParameter("@markedTime",SqlDbType.SmallDateTime,4),
                new SqlParameter("@recorderID",SqlDbType.Int,4)};

            parms[0].Value = obj.TotalScore;
            parms[1].Value = obj.Remark;
            parms[2].Value = obj.Marker.Name;
            parms[3].Value = obj.MarkedTime;
            parms[4].Value = obj.TestRecorder.RecorderID;

            SqlConnection conn = new SqlConnection(DBHelper.connStr);
            SqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction("insertTestMark");
                DBHelper.Insert(trans, "UP_T_TestMark_Insert", parms);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void InsertForRandomSelection(int recorderID)
        {
            SqlParameter[] parms ={
                new SqlParameter("@recorderID",recorderID)};
            DBHelper.Insert("UP_TestMark_Insert_RandomSelection", parms);
        }
        public void Update(TestMark obj)
        {
        }
        public void Delete(string id)
        {
        }
        public TestMark SelectByID(string id)
        {
            return null;
        }

        public List<TestMark> SelectList()
        {
            return null;
        }

        public List<TestMark> SelectListByUserID(string userID)
        {
            SqlParameter[] parms ={
                new SqlParameter("@userID",SqlDbType.VarChar,30)
            };
            parms[0].Value = userID;

            List<TestMark> list = new List<TestMark>();

            using (SqlDataReader dr = DBHelper.Select("UP_T_TestMarks_GetListByUserID", parms))
            {
                while (dr.Read())
                {
                    TestMark testMark = new TestMark();
                    try
                    {
                        testMark.TestRecorder.RecorderID = Convert.ToInt32(dr["recorderID"]);
                        testMark.TestRecorder.Test.TestID = Convert.ToInt32(dr["testID"]);
                        testMark.TestRecorder.Test.TestName = dr["testName"].ToString();
                        testMark.TestRecorder.Test.TotalScores = Convert.ToInt16(dr["paperScore"]);
                        testMark.TestRecorder.Test.PassScores = Convert.ToInt32(dr["passScores"]);
                        testMark.TestRecorder.Tester.UserID = userID;
                        testMark.TestRecorder.Tester.Name = dr["name"].ToString();
                        testMark.Remark = dr["remark"].ToString();
                        testMark.TestRecorder.Marked = Convert.ToBoolean(dr["marked"]);
                        testMark.TestRecorder.HasTested = Convert.ToBoolean(dr["hasTested"]);
                        testMark.Marker.Name = dr["marker"].ToString();
                        if (null != dr["totalScore"] && !string.IsNullOrEmpty(dr["totalScore"].ToString()))
                            testMark.TotalScore = Convert.ToUInt16(dr["totalScore"]);

                        testMark.HasPassed = (testMark.TotalScore - testMark.TestRecorder.Test.PassScores) >= 0 ? true : false;
                        testMark.MarkID = Convert.ToInt32(dr["markID"]);
                    }
                    catch { }
                    list.Add(testMark);
                }
            }

            return list;
        }

        public List<TestMark> SelectListByUserName(string name)
        {
            SqlParameter[] parms ={
                new SqlParameter("@name",SqlDbType.NVarChar,30)
            };
            parms[0].Value = name;

            List<TestMark> list = new List<TestMark>();

            using (SqlDataReader dr = DBHelper.Select("UP_T_TestMarks_GetListByUserName", parms))
            {
                while (dr.Read())
                {
                    TestMark testMark = new TestMark();
                    try
                    {
                        testMark.TestRecorder.RecorderID = Convert.ToInt32(dr["recorderID"]);
                        testMark.TestRecorder.Test.TestID = Convert.ToInt32(dr["testID"]);
                        testMark.TestRecorder.Test.TestName = dr["testName"].ToString();
                        testMark.TestRecorder.Test.TotalScores = Convert.ToInt16(dr["paperScore"]);
                        testMark.TestRecorder.Test.PassScores = Convert.ToInt32(dr["passScores"]);
                        testMark.TestRecorder.Tester.UserID = dr["userID"].ToString();
                        testMark.TestRecorder.Tester.Name = dr["name"].ToString();
                        testMark.Remark = dr["remark"].ToString();
                        testMark.TestRecorder.Marked = Convert.ToBoolean(dr["marked"]);
                        testMark.TestRecorder.HasTested = Convert.ToBoolean(dr["hasTested"]);
                        testMark.Marker.Name = dr["marker"].ToString();
                        if (null != dr["totalScore"] && !string.IsNullOrEmpty(dr["totalScore"].ToString()))
                            testMark.TotalScore = Convert.ToUInt16(dr["totalScore"]);

                        testMark.HasPassed = (testMark.TotalScore - testMark.TestRecorder.Test.PassScores) >= 0 ? true : false;
                        testMark.MarkID = Convert.ToInt32(dr["markID"]);
                    }
                    catch { }
                    list.Add(testMark);
                }
            }

            return list;
        }

        public List<TestMark> SelectListByTestID(int testID)
        {
            SqlParameter[] parms ={
                new SqlParameter("@testID",SqlDbType.Int,4)
            };
            parms[0].Value = testID;

            List<TestMark> list = new List<TestMark>();

            using (SqlDataReader dr = DBHelper.Select("UP_T_TestMarks_GetListByTestID", parms))
            {
                while (dr.Read())
                {
                    TestMark testMark = new TestMark();
                    testMark.TestRecorder.RecorderID = Convert.ToInt32(dr["recorderID"]);
                    testMark.TestRecorder.Test.TestID = testID;
                    testMark.TestRecorder.Test.TestName = dr["testName"].ToString();
                    testMark.TestRecorder.Tester.Name = dr["testerName"].ToString();
                    testMark.TestRecorder.Tester.Department.DeptName = dr["deptName"].ToString();
                    testMark.TestRecorder.Test.TotalScores = Convert.ToInt32(dr["totalScores"]);
                    testMark.TestRecorder.Test.PassScores = Convert.ToInt32(dr["passScores"]);
                    if (null != dr["testerTotalScore"] && !string.IsNullOrEmpty(dr["testerTotalScore"].ToString()))
                        testMark.TotalScore = Convert.ToUInt16(dr["testerTotalScore"]);
                    
                    testMark.TestRecorder.Marked = Convert.ToBoolean(dr["marked"]);
                    testMark.Marker.Name = dr["marker"].ToString();
                   
                    testMark.HasPassed = (testMark.TotalScore - testMark.TestRecorder.Test.PassScores) >= 0 ? true : false;

                    list.Add(testMark);
                }
            }

            return list;
        }

    }
}
