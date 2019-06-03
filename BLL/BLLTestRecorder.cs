//  @ Project : TestOnline
//  @ File Name : TestRecorder.cs
//  @ Date : 2009-3-12
//  @ Author : ruimingde
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Entity;
using DAL;

namespace BLL
{
    public class BLLTestRecorder
    {
        private static readonly DBTestRecorder dbTestRecorder=new DBTestRecorder();
        
        public void CreateTestRecorder(TestRecorder testRecorder)
        {
        }
        public TestRecorder GetTestRecorderByID(int recorderID)
        {
            return dbTestRecorder.SelectByID(recorderID.ToString());
        }

        
        public List<TestRecorder> GetTestRecorderListByTestID(string testID)
        {
            if(string.IsNullOrEmpty(testID))
                 return null;
             int id = 0;
             if (Int32.TryParse(testID, out id))
             {
                 return dbTestRecorder.SelectListByTestID(id);
             }
             else
                 return null;
        }

        public PageList<TestRecorder> GetTestRecorderListByTestID(int testID, int pageNum, int pageSize)
        {
            return new PageList<TestRecorder>(GetTestRecorderListByTestID(testID.ToString()), pageNum, pageSize);
        }

        public List<TestRecorder> GetTestRecorderListByUserID(string userID)
        {
            if(string.IsNullOrEmpty(userID))
                return null;

            return dbTestRecorder.SelectListByUserID(userID);
        }
        public TestRecorder GetTestRecorderListBy(string testID, string userID)
        {
            int _testID = 0;
            if (!int.TryParse(testID, out _testID))
                throw new Exception("testID不是整型数字！");

            return dbTestRecorder.SelectBy(_testID, userID);
        }
        public void RemoveTestRecorderByID(int recorderID)
        {
        }

        public void AutoSaveAnswer(string testID,string userID, string testerAnswer)
        {
            int _testID = 0;
            if (!int.TryParse(testID, out _testID))
                throw new Exception("testID不是整型数字！");

            this.testID = _testID;
            this.userID = userID;
            this.answer = testerAnswer;

            ThreadStart start=new ThreadStart(ThreadAutoSaveAnswer);
            Thread th = new Thread(start);
            th.Start();
        }
        private int testID;
        private string userID;
        private string answer;
        private string submitType;
        private DateTime submitTestTime;
        private void ThreadAutoSaveAnswer()
        {
            dbTestRecorder.Update(testID,userID, answer);
        }
        private void ThreadAutoSubmitTest()
        {
            dbTestRecorder.Update(testID, userID, answer, submitType, submitTestTime);
        }

        public void AutoSubmitTest(string testID, string userID, string testerAnswer,  DateTime submitTestTime)
        {
            int _testID = 0;
            if (!int.TryParse(testID, out _testID))
                throw new Exception("testID不是整型数字！");

            this.testID = _testID;
            this.userID = userID;
            this.answer = testerAnswer;
            this.submitType = "自动提交";
            this.submitTestTime = submitTestTime;

            ThreadStart start = new ThreadStart(ThreadAutoSubmitTest);
            Thread thr= new Thread(start);
            thr.Start();
        }
        public void SubmitTest(string testID,string userID, string testerAnswer, DateTime submitTestTime)
        {
            int _testID = 0;
            if (!int.TryParse(testID, out _testID))
                throw new Exception("testID不是整型数字！");

            dbTestRecorder.Update(_testID, userID, testerAnswer, "手工提交", submitTestTime);
        }

        public void SaveAnswer(string testID, string userID, string testerAnswer)
        {
            int _testID = 0;
            if (!int.TryParse(testID, out _testID))
                throw new Exception("testID不是整型数字！");

            dbTestRecorder.Update(_testID, userID, testerAnswer);
        }

        public void ModifyBeginTest(string testID,string userID,DateTime beginTime)
        {
                        int _testID = 0;
            if (!int.TryParse(testID, out _testID))
                throw new Exception("testID不是整型数字！");

            dbTestRecorder.Update_BeginTest(_testID, userID, beginTime);
        }

        public void CreateTestRecorderAnswer(int recorderID)
        {
            dbTestRecorder.InsertTestRecorderAnswer(recorderID);
        }

        public void CreateTestRecorderAnswer(int recorderID, List<SubjectOfFillBlank> list1, List<SubjectOfJudge> list2, List<SubjectOfSingleSelection> list3, List<SubjectOfMultiSelection> list4, List<SubjectOfSimpleAnswer> list5)
        {
            dbTestRecorder.InsertTestRecorderAnswer(recorderID, list1, list2, list3, list4, list5);
        }

        public void ModifyTestRecorderAnswer(int recorderID, List<SubjectOfFillBlank> list1, List<SubjectOfJudge> list2, List<SubjectOfSingleSelection> list3, List<SubjectOfMultiSelection> list4, List<SubjectOfSimpleAnswer> list5)
        {
            dbTestRecorder.UpdateTestRecorderAnswer(recorderID, list1, list2, list3, list4, list5);
        }

        public void SaveTestRecorderAnswer_Scores(int recorderID, List<SubjectOfFillBlank> list1, List<SubjectOfJudge> list2, List<SubjectOfSingleSelection> list3, List<SubjectOfMultiSelection> list4, List<SubjectOfSimpleAnswer> list5)
        {
            dbTestRecorder.UpdateTestRecorderAnswer_Scores(recorderID, list1, list2, list3, list4, list5);
        }
    }
}
