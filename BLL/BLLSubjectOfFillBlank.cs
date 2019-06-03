using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
    public class BLLSubjectOfFillBlank: IBLLSubject<SubjectOfFillBlank>
    {
        private DBSubjectOfFillBlank db = new DBSubjectOfFillBlank();

        #region IBLLSubject<SubjectOfFillBlank> Members

        public void CreateSubject(SubjectOfFillBlank subject)
        {
            db.Insert(subject);
        }

        public List<SubjectOfFillBlank> GetSubjectList()
        {
            List<SubjectOfFillBlank> list = db.SelectList();

            foreach (SubjectOfFillBlank subject in list)
            {
                subject.Question = subject.Question.Replace("（）", "<input type='text' class='txtBlank'/>");
            }

            return list;
        }

        public PageList<SubjectOfFillBlank> GetSubjectList(int pageNum, int pageSize)
        {
            return new PageList<SubjectOfFillBlank>(GetSubjectList(), pageNum, pageSize);
        }

        public List<SubjectOfFillBlank> GetSubjectList(int cateID)
        {
            List<SubjectOfFillBlank> list = db.SelectList(cateID);

            foreach (SubjectOfFillBlank subject in list)
            {
                subject.Question = subject.Question.Replace("（）", "<input type='text' class='txtBlank'/>");
            }

            return list;
        }

        public List<SubjectOfFillBlank> GetSubjectList(List<int> idList)
        {
            List<SubjectOfFillBlank> list = db.SelectList(idList);

            foreach (SubjectOfFillBlank subject in list)
            {
                subject.Question = subject.Question.Replace("（）", "<input type='text' class='txtBlank'/>");
            }

            return list;
        }

        public void DeleteSubject(int id)
        {
            db.Delete(id.ToString());
        }

        public void ModifySubject(SubjectOfFillBlank subject)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region  download
        #endregion
    }
}
