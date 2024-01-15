using NmbrsDemo.Models;
using System.Text.Json;
using NmbrsDemo.Database;

namespace NmbrsDemo.Logic;


public static class EmployeeInfo
{
    public static List<EmployeeBasicInfo> GetEmployeeList()
    {
        return EmployeeDB.GetEmployeeInfoList();
    }

    public static bool AddNewEmployee(EmployeeBasicInfo newEmployee)
    {
        return EmployeeDB.AddEmployeeInfo(newEmployee);
    }

    public static bool RemoveEmployeeByID(string employeeId)
    {
        return EmployeeDB.RemoveEmployeeById(employeeId);
    }
}
