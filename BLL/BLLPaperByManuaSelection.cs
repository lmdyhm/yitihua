using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
   public  class BLLPaperByManuaSelection
    {
       private DBPaperByManualSelection db = new DBPaperByManualSelection();

       public void CreatePaper(PaperByManualSelection paper)
       {
           db.Insert(paper);
       }

       public List<PaperByManualSelection> GetPaperList()
       {
           return db.SelectList();
       }

       public PageList<PaperByManualSelection> GetPaperList(int pageNum, int pageSize)
       {
           return new PageList<PaperByManualSelection>(GetPaperList(), pageNum, pageSize);
       }

       public List<PaperByManualSelection> GetPaperList(int deptID)
       {
           if (deptID <= 0)
               return null;

           return db.SelectList(deptID);
       }

       private void ChangeFillBlankSubject(List<SubjectOfFillBlank> fillBlankList)
       {
           if (fillBlankList != null)
           {
               int index = 0;
               foreach (SubjectOfFillBlank subject in fillBlankList)
               {
                   subject.Question = subject.Question.Replace("（）", "<input type='text' id='txtFillBlankAnswer" + (++index) + "' name='txtFillBlankAnswer" + index + "'  class='txt' size='20' runat='server' />");
               }
           }
       }

       /// <summary>
       /// 不含分数
       /// </summary>
       public PaperByManualSelection GetPaper(int paperID)
       {
           PaperByManualSelection paper = db.SelectByID(paperID.ToString());
           ChangeFillBlankSubject(paper.FillBlankList);
           return paper;
       }

       /// <summary>
       /// 含分配的分数
       /// </summary>
       public PaperByManualSelection GetPaper(int testID, int paperID)
       {
           PaperByManualSelection paper = db.SelectByID(testID, paperID);
           ChangeFillBlankSubject(paper.FillBlankList);
           return paper;
       }

       /// <summary>
       /// 含考生的回答
       /// </summary>
       public PaperByManualSelection GetPaper(int testID, int paperID, int recorderID)
       {
           PaperByManualSelection paper = db.SelectByID(testID, paperID);
           if(paper!=null)
             new DBTestRecorder().SetTestRecorderAnswer(recorderID, paper.FillBlankList, paper.JudgeList, paper.SingleSelectionList, paper.MultiSelectionList, paper.SimpleAnswerList);
           
           ChangeFillBlankSubject(paper.FillBlankList);
           return paper;
       }

       public void DeletePaper(int paperID)
       {
           db.Delete(paperID.ToString());
       }
    }
}
