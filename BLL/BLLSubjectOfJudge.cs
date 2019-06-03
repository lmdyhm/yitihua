using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
   public  class BLLSubjectOfJudge : IBLLSubject<SubjectOfJudge>
    {
       private DBSubjectOfJudge db = new DBSubjectOfJudge();

        #region IBLLSubject<SubjectOfJudge> Members

        public void CreateSubject(SubjectOfJudge subject)
        {
            db.Insert(subject);
        }

        public List<SubjectOfJudge> GetSubjectList()
        {
            List<SubjectOfJudge> list = db.SelectList();
            return list;
        }

        public PageList<SubjectOfJudge> GetSubjectList(int pageNum, int pageSize)
        {
            return new PageList<SubjectOfJudge>(GetSubjectList(), pageNum, pageSize);
        }

        public List<SubjectOfJudge> GetSubjectList(int cateID)
        {
            List<SubjectOfJudge> list = db.SelectList(cateID);
            return list;
        }

        public List<SubjectOfJudge> GetSubjectList(List<int> idList)
        {
            if (idList == null || idList.Count == 0)
                return null;

            return db.SelectList(idList);
        }

        public void DeleteSubject(int id)
        {
            db.Delete(id.ToString());
        }

        public void ModifySubject(SubjectOfJudge subject)
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
