using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;

namespace Tool
{
   public static  class JscriptHelper
    {
        /// <summary>
        /// 默认高度：600，宽度：800
        /// </summary>
        public static void OpenNewPage(string url)
        {
            OpenNewPage(url, string.Empty, 600, 800);
        }

       /// <summary>
       /// 默认高度：600，宽度：800
       /// </summary>
       public static void OpenNewPage(string url, string title)
       {
           OpenNewPage(url, title, 600, 800);
       }

       public static void OpenNewPage(string url, int height, int width)
       {
           OpenNewPage(url,string.Empty,height, width);
       }

       public static void OpenNewPage(string url, string title,int height,int width)
       {
           string script = string.Format("<script language='javascript'>window.open('{0}','{1}','height={2}, width={3}, top=0,left=0 , toolbar =no, menubar=no, scrollbars=yes, resizeable=yes, location=no, status=no')</script>", url, title,height,width);

           HttpContext.Current.Response.Write(script);
       }

       public static void ClosePage(string alertTitle)
       {
           string script = string.Format("<script language='javascript'>alert('{0}'); window.opener=null; window.open('','_self','');  window.close();</script>", alertTitle);

           HttpContext.Current.Response.Write(script);
           HttpContext.Current.Response.End();
       }

       public static void Alert(string alertTitle)
       {
           string script = string.Format("<script language='javascript'>alert('{0}')</script>", alertTitle);

           HttpContext.Current.Response.Write(script);
       }
    }
}
