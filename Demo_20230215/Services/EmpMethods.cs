using Demo_20230215.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_20230215.Services
{
    public class EmpMethods
    {
        /// <summary>
        /// 取得Default Employee Data
        /// </summary>
        /// <returns>List的員工物件</returns>
        public List<EmployeeModel> GetEmpData()
        {
            List<EmployeeModel> employeeModels= new List<EmployeeModel>();
            employeeModels.Add(new EmployeeModel() { EmpNo = "CT3040", Name = "人", Ext = 1683 });
            employeeModels.Add(new EmployeeModel() { EmpNo = "FG4481", Name = "我", Ext = 2956 });
            employeeModels.Add(new EmployeeModel() { EmpNo = "FG4482", Name = "你", Ext = 1783 });
            



            return employeeModels;
        }
    }
}