//  @ Project : TestOnline
//  @ File Name : Paper.cs
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
    public class BLLPaper
    {
        private static readonly DBPaper dbPaper=new DBPaper();

        public void CreatePaper(Paper paper)
        {
            dbPaper.Insert(paper);
        }
        public List<Paper> GetPaperList()
        {
            return dbPaper.SelectList();
        }

        public PageList<Paper> GetPaperList(int pageNum, int pageSize)
        {
            return new PageList<Paper>(dbPaper.SelectList(), pageNum, pageSize);
        }

        public List<Paper> GetPaperListByDeptID(int deptID)
        {
            return dbPaper.SelectListByDeptID(deptID);
        }
        public Paper GetPaperByID(string paperID)
        {
            int id = 0;
            if (!int.TryParse(paperID, out id))
                return null;

            return dbPaper.SelectByID(paperID);
        }

        public void ModifyPaper(Paper paper)
        {
            dbPaper.Update(paper);
        }
        public void DeletePaper(int paperID)
        {
            dbPaper.Delete(paperID.ToString());
        }
    }
}