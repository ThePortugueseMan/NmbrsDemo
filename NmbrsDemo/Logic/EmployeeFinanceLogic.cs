using NmbrsDemo.Models;
using System.Text.Json;
using NmbrsDemo.Database;

namespace NmbrsDemo.Logic;


public static class EmployeeFinanceLogic
{
    public static IEmployeeFinance GetEmployeeFinanceFromId(string id)
    {
        return EmployeeDB.GetEmployeeFinanceById(id);
    }

    public static bool InsertOrUpdateGrossAnnualSalaryById(string id, decimal grossAnnualSalary)
    {
        return EmployeeDB.InsertOrUpdateGrossAnnualSalaryById(id, grossAnnualSalary);
    }

    public static bool RemoveEmployeeByID(string employeeId)
    {
        return EmployeeDB.RemoveEmployeeById(employeeId);
    }
}
