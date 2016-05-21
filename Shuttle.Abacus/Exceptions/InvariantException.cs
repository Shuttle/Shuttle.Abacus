using System;

namespace Shuttle.Abacus
{
    public class InvariantException : Exception
    {
        public InvariantException(string message) : base(message)
        {
        }
    }
}
