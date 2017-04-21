using System;

namespace Shuttle.Abacus.Infrastructure
{
    public class InvalidStateException : Exception
    {
        public InvalidStateException(string message) : base(message)
        {
        }
    }
}
