using System;
using System.Collections.Generic;
using System.Text;

using DAL;
using Entity;
using BLL;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            new BLLPaperByRandomSelection().GetPaper(1);

            string userPwd = "51aspx";
            string encryptCode = Tool.StringHelper.EncryptPwd(userPwd);

            Console.WriteLine(encryptCode);

            Console.Read();
        }
    }
}
