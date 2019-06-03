//  @ Project : TestOnline
//  @ File Name : CountMark.cs
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
    public class BLLCountMark
    {
        private static readonly DBCountMark dbCountMark=new DBCountMark();
        public CountMark GetCountMark(string testID)
        {
            int id = 0;
            if (!int.TryParse(testID, out id))
                return null;

            return dbCountMark.SelectCountMark(id);
           
        }
    }
}
