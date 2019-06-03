//  @ Project : TestOnline
//  @ File Name : Department.cs
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
    public class BLLDepartment
    {
        private static readonly DBDepartment dbDepartment = new DBDepartment();

        public void CreateDept(Department dept)
        {
            dbDepartment.Insert(dept);
        }
        public void ModifyDept(Department dept)
        {
            dbDepartment.Update(dept);
        }
        public void RemoveDept(string deptID)
        {
            if (string.IsNullOrEmpty(deptID))
                throw new Exception("����deptIDΪ�գ�");

            short id = 0;
            if (!Int16.TryParse(deptID, out id))
                throw new Exception("����deptID��Ϊ������");

            dbDepartment.Delete(deptID);
        }
        public List<Department> GetDeptList()
        {
           return dbDepartment.SelectList();
        }
        public List<Department> GetDeptListForDropList()
        {
            List<Department> deptList = new List<Department>();
            Department dept=new Department();
            dept.DeptID=-1;
            dept.DeptName="��ѡ����";
            deptList.Add(dept);

            List<Department> deptList2=this.GetDeptList();
            if (deptList2.Count > 0)
                deptList.AddRange(deptList2);

            return deptList;
        }

        public Department GetDeptByID(string deptID)
        {
            if (string.IsNullOrEmpty(deptID))
                return null;

            return dbDepartment.SelectByID(deptID);
        }
    }
}