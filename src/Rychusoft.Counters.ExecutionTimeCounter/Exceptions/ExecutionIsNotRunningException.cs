using System;

namespace Rychusoft.Counters.ExecutionTime.Exceptions
{
    public class ExecutionIsNotRunningException : Exception
    {
        public ExecutionIsNotRunningException(string message) : base(message)
        {
        }

        public ExecutionIsNotRunningException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ExecutionIsNotRunningException()
        {
        }
    }
}