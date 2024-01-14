using Microsoft.Data.Sqlite;
using NmbrsDemo.Models;
using System.Data;

namespace NmbrsDemo.Database
{
    public static class EmployeeDB
    {
        static string connectionString = "Data Source=employee.db";
        static bool isInitialized = false;

        private static void InitializeDb()
        {
            using (var connection = new SqliteConnection(connectionString)) 
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "CREATE TABLE IF NOT EXISTS EmployeeInfo (" +
                    "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "FirstName TEXT," +
                    "LastName TEXT);";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
            isInitialized = true;
        }

        public static List<Employee> GetEmployeeInfoList()
        {
            if (!isInitialized) InitializeDb();

            List<Employee> employeeList = new List<Employee>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "SELECT * FROM EmployeeInfo";

                using (var reader = tableCmd.ExecuteReader())
                {
                    Employee auxEmployee;
                    while(reader.Read())
                    {
                        auxEmployee = new Employee()
                        {
                            EmployeeId = ((Int64)reader.GetValue("Id")).ToString(),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName")
                        };
                        employeeList.Add(auxEmployee);
                    }
                }

                connection.Close();
            }

            return employeeList;
        }

        public static bool AddEmployeeInfo(Employee employee) 
        {
            if (!isInitialized) InitializeDb();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "INSERT INTO EmployeeInfo" +
                    " (FirstName, LastName)" +
                    $" VALUES (\'{employee.FirstName}\', \'{employee.LastName}\');" +
                    $" SELECT * FROM EmployeeInfo " +
                    $" WHERE Id = last_insert_rowid();";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            return true;
        }

        public static bool RemoveEmployeeById(string employeeId)
        {
            if (!isInitialized) InitializeDb();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "DELETE FROM EmployeeInfo" +
                    $" WHERE Id = {employeeId};";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            return true;
        }
    }
}
