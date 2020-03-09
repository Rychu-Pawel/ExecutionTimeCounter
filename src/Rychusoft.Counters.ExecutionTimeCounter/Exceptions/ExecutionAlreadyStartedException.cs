using System;

namespace Rychusoft.Counters.ExecutionTime.Exceptions
{
    public class ExecutionAlreadyStartedException : Exception
    {
        public ExecutionAlreadyStartedException(string message) : base(message)
        {
        }

        public ExecutionAlreadyStartedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ExecutionAlreadyStartedException()
        {
        }
    }
}