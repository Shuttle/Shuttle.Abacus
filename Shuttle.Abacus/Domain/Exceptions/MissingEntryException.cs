using System;

namespace Shuttle.Abacus
{
    public class MissingEntryException : Exception
    {
        public MissingEntryException(string message) : base(message)
        {
        }
    }
}
