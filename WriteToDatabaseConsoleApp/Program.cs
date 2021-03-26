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

            CreateRecords(recordsPath);

            using (var csvRecords = new CsvEnumerable<Record>(recordsPath))
            {
                var consoleLogger = new Logger();

                using (IRepository<Record> repository = LoggingProxy<IRepository<Record>>.CreateInstance(new MSSQLLocalDBRepository(), consoleLogger))
                {
                    var tasks = new List<Task<int>>();

                    foreach (var record in csvRecords)
                    {
                        Console.WriteLine($"Id = {record.Id}, Name = {record.Name}");
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
                    Name = "C#", Id = 0 },
                new Record {
                    Name = "Delphi", Id = 11 },
                new Record {
                    Name = "JavaScript", Id = 7  },
                new Record {
                    Name = "Python", Id = 3 },
                new Record {
                    Name = "C++", Id = 5 }
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
