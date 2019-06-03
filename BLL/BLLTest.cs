//  @ Project : TestOnline
//  @ File Name : Test.cs
//  @ Date : 2009-3-12
//  @ Author : ruimingde
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
    public class BLLTest
    {
        private static readonly DBTest dbTest = new DBTest();

        public void CreateTest(Test test)
        {
            dbTest.Insert(test);
        }

        public void CreateTest(Test test, int fillBlankScoresOfEveryone, int judgeScoresOfEveryone, int singleSelectionScoresOfEveryone, int multiSelectionScoresOfEveryone, List<SubjectOfSimpleAnswer> simpleAnswerScoresList)
        {
            dbTest.Insert(test, fillBlankScoresOfEveryone, judgeScoresOfEveryone, singleSelectionScoresOfEveryone, multiSelectionScoresOfEveryone, simpleAnswerScoresList);
        }

        public Test GetTestByID(string testID)
        {
            int id = 0;
            if (!Int32.TryParse(testID, out id))
                return null;
            
            return dbTest.SelectByID(testID);
        }
        public List<Test> GetTestList()
        {
            List<Test> testList= dbTest.SelectList();
            return testList;
        }

        public PageList<Test> GetTestList(int pageNum, int pageSize)
        {
            return new PageList<Test>(GetTestList(), pageNum, pageSize);
        }

        public List<Test> GetTestListForMarks()
        {
            return dbTest.SelectListForMarks();
        }

        public List<Test> GetTestListByUserID(string userID)
        {
            return dbTest.SelectListByUserID(userID);
        }
        public void ModifyTest(Test test)
        {
            dbTest.Update(test);
        }
        public void RemoveTest(int testID)
        {
            dbTest.Delete(testID.ToString());
        }


    }
}
