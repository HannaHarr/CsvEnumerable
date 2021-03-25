using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingLibrary
{
    public interface IMessagePublisher
    {
        public void Publish(string message);
    }
}
