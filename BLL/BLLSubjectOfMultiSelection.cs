using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
    public class BLLSubjectOfMultiSelection : IBLLSubject<SubjectOfMultiSelection>
    {
        private DBSubjectOfMultiSelection db = new DBSubjectOfMultiSelection();
        #region IBLLSubject<SubjectOfMultiSelection> Members

        public void CreateSubject(SubjectOfMultiSelection subject)
        {
            db.Insert(subject);
        }

        public List<SubjectOfMultiSelection> GetSubjectList()
        {
            return db.SelectList();
        }

        public PageList<SubjectOfMultiSelection> GetSubjectList(int pageNum, int pageSize)
        {
            return new PageList<SubjectOfMultiSelection>(GetSubjectList(), pageNum, pageSize);
        }

        public List<SubjectOfMultiSelection> GetSubjectList(int cateID)
        {
            return db.SelectList(cateID);
        }

        public List<SubjectOfMultiSelection> GetSubjectList(List<int> idList)
        {
            if (null == idList || idList.Count == 0)
                return null;
            return db.SelectList(idList);
        }

        public void DeleteSubject(int id)
        {
            db.Delete(id.ToString());
        }

        public void ModifySubject(SubjectOfMultiSelection subject)
        {
            throw new NotImplementedException();
        }

        public int GetSubjectCount(int cateID)
        {
            return db.SelectCount(cateID);
        }
        #endregion
    }
}
