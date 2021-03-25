using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WorkWithDatabaseLibrary
{
    public static class Class1
    {
        // строка подключения к бд
        // Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ITechArt\programs\WriteToDatabase\WorkWithDatabaseLibrary\WorkDatabase.mdf;Integrated Security=True
        private static readonly string dataSource = @"(LocalDB)\MSSQLLocalDB";
        // private static readonly string databaseFilename = $@"{Environment.CurrentDirectory}\WorkDatabase.mdf";
        private static readonly string databaseFilename = @"D:\ITechArt\programs\WriteToDatabase\WorkWithDatabaseLibrary\WorkDatabase.mdf";

        public static SqlConnection GetDBConnection()
        {
            string connString = $@"Data Source={dataSource};AttachDbFilename={databaseFilename};Integrated Security=True";

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }
    }
}
