using System;
using System.Globalization;
using System.IO;
using WorkWithDatabaseLibrary;
using System.Collections.Generic;
using CsvHelper;
using LoggingLibrary;
using System.Threading;
using System.Threading.Tasks;

namespace WriteToDatabaseConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var recordsPath = @$"{Environment.CurrentDirectory}/text.csv";
            var dataSource = @"(LocalDB)\MSSQLLocalDB";
            var databaseFilename = @$"{Directory.GetCurrentDirectory()}\WorkDatabase.mdf";
            var connString = $@"Data Source={dataSource};AttachDbFilename={databaseFilename};Integrated Security=True;MultipleActiveResultSets=True";

            CreateRecords(recordsPath);

            using (var csvRecords = new CsvEnumerable<Record>(recordsPath))
            {
                var consoleLogger = new Logger();

                using (IRepository<Record> repository = LoggingProxy<IRepository<Record>>.CreateInstance(new MSSQLLocalDBRepository(connString), consoleLogger))
                {
                    var tasks = new List<Task<int>>();

                    foreach (var record in csvRecords)
                    {
                        Console.WriteLine($"Name = {record.Name}, Number = {record.Number}");
                        tasks.Add(repository.CreateAsync(record));
                    }

                    Task.WaitAll(tasks.ToArray());
                }
            }
        }

        private static void CreateRecords(string filePath)
        {
            List<Record> records = new List<Record>
            {
                new Record {
                    Name = "C#", Number = 0 },
                new Record {
                    Name = "Delphi", Number = 11 },
                new Record {
                    Name = "JavaScript", Number = 7  },
                new Record {
                    Name = "Python", Number = 3 },
                new Record {
                    Name = "C++", Number = 5 }
            };

            using (var writer = new StreamWriter(filePath))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
            }
        }
    }
}
