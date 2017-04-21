using System;

namespace Shuttle.Abacus
{
    public class CircularReferenceException : Exception
    {
        public CircularReferenceException(string message) : base(message)
        {
        }
    }
}
