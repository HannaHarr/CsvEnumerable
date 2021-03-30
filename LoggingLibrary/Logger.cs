using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingLibrary
{
    public class Logger : ILogger
    {
        private IMessagePublisher messagePublisher;

        public Logger()
        {
            messagePublisher = new ConsolePublisher();
        }

        public Logger(IMessagePublisher publisher)
        {
            messagePublisher = publisher;
        }

        public void Error(string message)
        {
            messagePublisher.Publish($"Error: {message}");
        }

        public void Error(Exception ex)
        {
            messagePublisher.Publish($"Error: {ex.Message}");
        }

        public void Info(string message)
        {
            messagePublisher.Publish($"Info: {message}");
        }

        public void Warning(string message)
        {
            messagePublisher.Publish($"Warning: {message}");
        }
    }
}
