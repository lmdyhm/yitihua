//  @ Project : TestOnline
//  @ File Name : DBOperation.cs
//  @ Date : 2009-3-12
//  @ Author : ruimingde

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public interface DBOperation<T>
    {
        void Insert(T obj);
        void Update(T obj);
        void Delete(string id);
        T SelectByID(string id);
        List<T> SelectList();
    }
}