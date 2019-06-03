using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
    public class BLLSubjectOfSingleSelection : IBLLSubject<SubjectOfSingleSelection>
    {
        private DBSubjectOfSingleSelection db = new DBSubjectOfSingleSelection();

        #region IBLLSubject<SubjectOfSingleSelection> Members

        public void CreateSubject(SubjectOfSingleSelection subject)
        {
            db.Insert(subject);
        }

        public List<SubjectOfSingleSelection> GetSubjectList()
        {
            return db.SelectList();
        }

        public PageList<SubjectOfSingleSelection> GetSubjectList(int pageNum, int pageSize)
        {
            return new PageList<SubjectOfSingleSelection>(GetSubjectList(), pageNum, pageSize);
        }

        public List<SubjectOfSingleSelection> GetSubjectList(int cateID)
        {
            return db.SelectList(cateID);
        }

        public List<SubjectOfSingleSelection> GetSubjectList(List<int> idList)
        {
            if (idList == null || idList.Count == 0)
                return null;
            return db.SelectList(idList);
        }

        public void DeleteSubject(int id)
        {
            db.Delete(id.ToString());
        }

        public void ModifySubject(SubjectOfSingleSelection subject)
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
