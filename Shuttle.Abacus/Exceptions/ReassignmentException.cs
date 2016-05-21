using System;

namespace Shuttle.Abacus
{
    public class ReassignmentException : Exception
    {
        public ReassignmentException(string message) : base(message)
        {
        }
    }
}
