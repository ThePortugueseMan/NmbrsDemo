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

        public static Employee AddEmployeeInfo(Employee employee) 
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
                    $" WHERE where Id = last_insert_rowid();";

                using (var reader = tableCmd.ExecuteReader())
                {
                    employee.EmployeeId = reader.GetString("Id");
                    employee.FirstName = reader.GetString("FirstName");
                    employee.LastName = reader.GetString("LastName");
                }

                connection.Close();
            }

            return employee;
        }
    }
}
