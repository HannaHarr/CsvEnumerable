using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingLibrary
{
    public class ConsolePublisher : IMessagePublisher
    {
        public void Publish(string message)
        {
            Console.WriteLine(message);
        }
    }
}
