using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;
using Tool;
namespace BLL
{
    public class BLLSubjectTypeCategory
    {
        private DBSubjectTypeCategory db = new DBSubjectTypeCategory();

        public void Create(SubjectTypeCategory category)
        {
            db.Insert(category);
        }

        public List<SubjectTypeCategory> GetListBySubjectType(SubjectType subjectType)
        {
            List<SubjectTypeCategory> list= db.SelectListBySubjectType(subjectType);
            List<SubjectTypeCategory> list2 = new List<SubjectTypeCategory>();
            list2.Add(new SubjectTypeCategory() { CateID = -1, CateName = "请选择题型分类" });
            foreach (SubjectTypeCategory type in list)
            {
                list2.Add(type);
            }
            return list2;
        }
    }
}
