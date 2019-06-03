//  @ Project : TestOnline
//  @ File Name : DBCountMark.cs
//  @ Date : 2009-3-12
//  @ Author : ruimingde

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Entity;

namespace DAL
{
    public class DBCountMark 
    {
        public CountMark SelectCountMark(int testID)
        {
            SqlParameter[] parm=
            {
                new SqlParameter("@testID",SqlDbType.Int,4)
            };
            parm[0].Value = testID;

            CountMark countMark = null;
            using (SqlDataReader dr = DBHelper.Select("UP_SelectCountMark", parm))
            {
                if (dr.Read())
                {
                    countMark = new CountMark();
                    countMark.TestName = dr["testName"].ToString();
                    countMark.TesterSum = Convert.ToInt32(dr["testerSum"]);
                    countMark.TesterSumDoTest = Convert.ToInt32(dr["testerSumDoTest"]);
                    countMark.PassedSum = Convert.ToInt32(dr["passedSum"]);
                }
            }

            return countMark;
        }
    }
}