using System;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
   public  class BLLPaperByRandomSelection
    {
       private DBPaperByRandomSelection db = new DBPaperByRandomSelection();

       public void CreatePaper(PaperByRandomSelection paper)
       {
           if ((paper.JudgeSum + paper.SingleSelectionSum + paper.MultiSelectionSum)==0)
           {
               throw new BLLException("3类题型的数量总和必须大于0！");
           }
           db.Insert(paper);
       }

       public List<PaperByRandomSelection> GetPaperList()
       {
           return db.SelectList();
       }

       public PageList<PaperByRandomSelection> GetPaperList(int pageNum, int pageSize)
       {
           return new PageList<PaperByRandomSelection>(GetPaperList(), pageNum, pageSize);
       }

       /// <summary>
       /// 除了试卷的基本信息外，也包含各类题库集合
       /// </summary>
       public PaperByRandomSelection GetPaper(int paperID)
       {
           if (paperID <= 0)
               return null;
           return db.SelectByID(paperID.ToString());
       }

       /// <summary>
       /// 除了试卷的基本信息外，也包含各类题库集合(含分配的分数)
       /// </summary>
       public PaperByRandomSelection GetPaper(int testID, int paperID)
       {
           return db.SelectByID(testID, paperID);
       }

       /// <summary>
       /// 只获取试卷的基本信息
       /// </summary>
       public PaperByRandomSelection GetPaper2(int paperID)
       {
           if (paperID <= 0)
               return null;
           return db.SelectByID2(paperID.ToString());
       }

        /// <summary>
        /// 获取考生的实际应考试卷和回答
        /// </summary>
       public PaperByRandomSelection GetPaper3(int testID, int recorderID)
       {
           return db.SelectByID3(testID, recorderID);
       }

       public void DeletePaper(int paperID)
       {
           db.Delete(paperID.ToString());
       }
    }
}
