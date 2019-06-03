using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Entity;
using DAL;

namespace BLL
{
    public interface IBLLSubject<T>
    {
        void CreateSubject(T subject);

        List<T> GetSubjectList();

        void DeleteSubject(int id);

        void ModifySubject(T subject);
    }
}
