using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CsvEnumerableLibrary
{
    public class CsvEnumerable<T> : IEnumerable<T>
    {
        private StreamReader streamReader;
        private CsvReader csvReader;

        public CsvEnumerable(string path)
        {
            streamReader = new StreamReader(path);
            csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return csvReader.GetRecords<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        ~CsvEnumerable()
        {
            csvReader.Dispose();

            streamReader.Close();
            streamReader.Dispose();
        }
    }
}
