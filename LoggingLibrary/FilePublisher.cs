using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LoggingLibrary
{
    public class FilePublisher : IMessagePublisher
    {
        private readonly string writePath;

        public FilePublisher()
        {
            writePath = @$"{Environment.CurrentDirectory}/Log.txt";
        }

        public FilePublisher(string path)
        {
            if (File.Exists(path))
            {
                writePath = @$"{path}/Log.txt";
            }
        }

        public FilePublisher(string path, string fileName)
        {
            if (File.Exists(path))
            {
                writePath = @$"{path}/{fileName}";
            }
        }

        public void Publish(string message)
        {
            using (var sw = new StreamWriter(writePath, true, Encoding.Default))
            {
                sw.WriteLine(message);
            }
        }
    }
}
