using System;

namespace Shuttle.Abacus.Infrastructure
{
    public class CircularReferenceException : Exception
    {
        public CircularReferenceException(string message) : base(message)
        {
        }
    }
}
