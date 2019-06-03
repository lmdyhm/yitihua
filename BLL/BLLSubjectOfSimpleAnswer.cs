using System;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
    public class BLLSubjectOfSimpleAnswer : IBLLSubject<SubjectOfSimpleAnswer>
    {
        private DBSubjectOfSimpleAnswer db = new DBSubjectOfSimpleAnswer();
        #region IBLLSubject<SubjectOfSimpleAnswer> Members

        public void CreateSubject(SubjectOfSimpleAnswer subject)
        {
            db.Insert(subject);
            
        }

        public List<SubjectOfSimpleAnswer> GetSubjectList()
        {
            return db.SelectList();
        }

        public PageList<SubjectOfSimpleAnswer> GetSubjectList(int pageNum, int pageSize)
        {
            return new PageList<SubjectOfSimpleAnswer>(GetSubjectList(), pageNum, pageSize);
        }

        public List<SubjectOfSimpleAnswer> GetSubjectList(int cateID)
        {
            return db.SelectList(cateID);
        }

        public List<SubjectOfSimpleAnswer> GetSubjectList(List<int> idList)
        {
            if (null == idList || idList.Count == 0)
                return null;
            return db.SelectList(idList);
        }

        public void DeleteSubject(int id)
        {
            db.Delete(id.ToString());
        }

        public void ModifySubject(SubjectOfSimpleAnswer subject)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
