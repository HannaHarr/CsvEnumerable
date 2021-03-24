using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CsvEnumerableLibrary
{
    public class CsvEnumerable<T> : IEnumerable<T>, IDisposable
    {
        private readonly StreamReader streamReader;
        private readonly CsvReader csvReader;

        private bool disposedValue;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    csvReader.Dispose();

                    streamReader.Dispose();
                }

                streamReader.Close();

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CsvEnumerable()
        {
            Dispose(false);
        }
    }
}
