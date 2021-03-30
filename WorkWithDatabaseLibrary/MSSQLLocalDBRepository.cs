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
        private bool disposedValue;

        private SqlConnection sqlConnection;

        public MSSQLLocalDBRepository(string connString)
        {
            sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
        }

        public Task<int> CreateAsync(Record record)
        {
            SqlParameter nameParam = new SqlParameter("@name", SqlDbType.NVarChar, 50);
            nameParam.Value = record.Name;

            SqlParameter numberParam = new SqlParameter("@number", SqlDbType.Int, 4);
            numberParam.Value = record.Number;

            SqlCommand insertCommand = new SqlCommand("INSERT INTO languages (lg_name, lg_number) " +
                                       "Values (@name, @number)", sqlConnection);

            insertCommand.Parameters.Add(nameParam);
            insertCommand.Parameters.Add(numberParam);

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
