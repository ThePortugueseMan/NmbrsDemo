using NmbrsDemo.Models;
using System.Text.Json;
using NmbrsDemo.Database;

namespace NmbrsDemo.Logic;


public static class EmployeeInfo
{
   public static List<Employee> GetEmployeeList()
   {
        string fileContents = File.ReadAllText("./Data/EmployeeList.json");

        List<Employee> employeeList = new List<Employee>();
        employeeList = JsonSerializer.Deserialize<List<Employee>>(fileContents);

        return employeeList;
    }

    public static bool AddNewEmployee(Employee newEmployee)
    {
        try
        {
            EmployeeDB.AddEmployeeInfo(newEmployee);

            List<Employee> currEmployeeList = GetEmployeeList();

            currEmployeeList.Add(newEmployee);

            string updatedContents = JsonSerializer.Serialize(currEmployeeList);

            File.WriteAllText("./Data/EmployeeList.json", updatedContents);

            return true;
        }

        catch(Exception ex)
        {
            return false;
        }
    }

    public static bool RemoveEmployeeByID(string employeeId)
    {
        try
        {
            List<Employee> currEmployeeList = GetEmployeeList();

            currEmployeeList.RemoveAll(e => e.EmployeeId == employeeId);

            UpdateEmployeeListDocument(currEmployeeList);

            return true;
        }

        catch (Exception ex)
        {
            return false;
        }
    }

    private static void UpdateEmployeeListDocument(List<Employee> updatedList)
    {
        string updatedContents = JsonSerializer.Serialize(updatedList);
        File.WriteAllText("./Data/EmployeeList.json", updatedContents);
    }
}
