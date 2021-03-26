using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithDatabaseLibrary
{
    public class MSSQLLocalDBRepository : IRepository<Record>
    {
        private static readonly string dataSource = @"(LocalDB)\MSSQLLocalDB";
        private static readonly string databaseFilename = @$"{Directory.GetCurrentDirectory()}\WorkDatabase.mdf";

        private bool disposedValue;

        private SqlConnection sqlConnection;

        public MSSQLLocalDBRepository()
        {
            string connString = $@"Data Source={dataSource};AttachDbFilename={databaseFilename};Integrated Security=True;MultipleActiveResultSets=True";

            sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();
        }

        public Task<int> CreateAsync(Record record)
        {
            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int, 4);
            idParam.Value = record.Id;

            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, 50);
            nameParam.Value = record.Name;

            SqlCommand insertCommand = new SqlCommand("INSERT INTO language (lg_id, lg_name) " +
                                       "Values (@id, @name)", sqlConnection);

            insertCommand.Parameters.Add(idParam);
            insertCommand.Parameters.Add(nameParam);

            return insertCommand.ExecuteNonQueryAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    sqlConnection.Dispose();
                }

                sqlConnection.Close();

                disposedValue = true;
            }
        }

        ~MSSQLLocalDBRepository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
