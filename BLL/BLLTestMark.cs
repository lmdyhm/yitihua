//  @ Project : TestOnline
//  @ File Name : TestRecorder.cs
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
    public class BLLTestMark
    {
        private static readonly DBTestMark dbTestMark=new DBTestMark();

        public void CreateTestMark(TestMark testMark)
        {
            dbTestMark.Insert(testMark);
        }

        public void CreateTestMarkForRandomSelection(int recorderID)
        {
            dbTestMark.InsertForRandomSelection(recorderID);
        }
        public TestMark GetTestMark(int markID)
        {
            return null;
        }

        public List<TestMark> GetMarkList(string userID)
        {
            if (string.IsNullOrEmpty(userID))
                return null;

            return dbTestMark.SelectListByUserID(userID);
        }

        public List<TestMark> SearchMarkByUserName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<TestMark>();

            return dbTestMark.SelectListByUserName(name);
        }

        public List<TestMark> GetMarkListByTestID(string testID)
        {
            int id = 0;
            if(!int.TryParse(testID,out id))
                return null;

            return dbTestMark.SelectListByTestID(id);
        }

        public PageList<TestMark> GetMarkListByTestID(int testID, int pageNum, int pageSize)
        {
            return new PageList<TestMark>(GetMarkListByTestID(testID.ToString()), pageNum, pageSize);
        }

        public void CreateTestMark(TestMark mark, List<SubjectOfFillBlank> list1, List<SubjectOfJudge> list2, List<SubjectOfSingleSelection> list3, List<SubjectOfMultiSelection> list4, List<SubjectOfSimpleAnswer> list5)
        {
            this.CreateTestMark(mark);
            new BLLTestRecorder().SaveTestRecorderAnswer_Scores(mark.TestRecorder.RecorderID, list1, list2, list3, list4, list5);
        }
    }
}
