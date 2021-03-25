using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using WorkWithDatabaseLibrary;
using System.Collections.Generic;

namespace WriteToDatabaseConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @$"{Environment.CurrentDirectory}/text.csv";

            CreateRecords(path);

            using (var csvRecords = new CsvEnumerable<Record>(path)) 
            {
                foreach (var record in csvRecords)
                {
                    Console.WriteLine($"Id = {record.Id}, Name = {record.Name}");
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

        private class Record
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
