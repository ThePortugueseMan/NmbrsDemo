using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using NmbrsDemo.Models;
using System.Collections.Generic;
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

                tableCmd.CommandText = 
                    "CREATE TABLE IF NOT EXISTS EmployeeInfo (" +
                    "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "FirstName TEXT," +
                    "LastName TEXT," +
                    "EmployeeTypeId INTEGER);" +
                    "CREATE TABLE if not EXISTS EmployeeType(" +
                    "EmployeeTypeId INTEGER," +
                    " Type TEXT);" +
                    "CREATE TABLE IF NOT EXISTS EmployeeFinance(" +
                    "EmployeeId INTEGER UNIQUE," +
                    "GrossAnnualSalary NUMERIC);";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
            isInitialized = true;
        }

        public static List<EmployeeBasicInfo> GetEmployeeInfoList()
        {
            if (!isInitialized) InitializeDb();

            List<EmployeeBasicInfo> employeeList = new List<EmployeeBasicInfo>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "SELECT * FROM EmployeeInfo";

                using (var reader = tableCmd.ExecuteReader())
                {
                    EmployeeBasicInfo auxEmployee;
                    while(reader.Read())
                    {
                        auxEmployee = new EmployeeBasicInfo()
                        {
                            EmployeeId = ((Int64)reader.GetValue("Id")).ToString(),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            EmployeeTypeId = ((Int64)reader.GetValue("EmployeeTypeId")).ToString()
                        };
                        employeeList.Add(auxEmployee);
                    }
                }

                connection.Close();
            }

            return employeeList;
        }

        public static bool AddEmployeeInfo(EmployeeBasicInfo employee) 
        {
            if (!isInitialized) InitializeDb();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "INSERT INTO EmployeeInfo" +
                    " (FirstName, LastName, EmployeeTypeId)" +
                    $" VALUES (\'{employee.FirstName}\', \'{employee.LastName}\',{employee.EmployeeTypeId});" +
                    $" SELECT * FROM EmployeeInfo " +
                    $" WHERE Id = last_insert_rowid();";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            InsertOrUpdateGrossAnnualSalaryById(employee.EmployeeId, 0);

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
                    $" WHERE Id = {employeeId};" +
                    " DELETE FROM EmployeeFinance " +
                    $" WHERE EmployeeId = {employeeId};";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            return true;
        }

        public static IEmployeeFinance GetEmployeeFinanceById(string employeeId)
        {
            if (!isInitialized) InitializeDb();

            object returnObj = null;

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "SELECT EF.GrossAnnualSalary, ET.EmployeeTypeId" +
                    " FROM EmployeeInfo as EI" +
                    " INNER JOIN EmployeeFinance as EF ON EF.EmployeeId = EI.Id" +
                    " INNER JOIN EmployeeType as ET ON ET.EmployeeTypeId = EI.EmployeeTypeId" +
                    $" WHERE EI.Id = {employeeId}";


                using (var reader = tableCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        decimal grossAnnualSalary = decimal.Parse(reader.GetValue(0).ToString());
                        int employeeType = int.Parse(reader.GetValue(1).ToString());
                        if(employeeType == (int)EmployeeBasicInfo.EmployeeType.Regular)
                        {
                            returnObj = new RegularEmployeeFinance(grossAnnualSalary);
                        }
                        else if(employeeType == (int)EmployeeBasicInfo.EmployeeType.Special)
                        {
                            returnObj = new SpecialEmployeeFinance(grossAnnualSalary);
                        }
                    }
                }

                if (returnObj == null) returnObj = new RegularEmployeeFinance(0);
                connection.Close();
                return (IEmployeeFinance)returnObj;
            }
        }

        public static bool InsertOrUpdateGrossAnnualSalaryById(string employeeId, decimal grossAnnualSalary)
        {
            bool success = false;
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "INSERT OR IGNORE INTO EmployeeFinance" +
                    $" VALUES ({employeeId},{grossAnnualSalary});" +
                    " UPDATE EmployeeFinance" +
                    $" SET GrossAnnualSalary = {grossAnnualSalary} WHERE EmployeeId = {employeeId}";

                if (tableCmd.ExecuteNonQuery() == 1) success = true;

                connection.Close();
            }
            return success;
        }

    }
}
