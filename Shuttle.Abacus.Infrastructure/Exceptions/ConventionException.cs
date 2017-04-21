using System;

namespace Shuttle.Abacus.Infrastructure
{
    public class ConventionException : Exception
    {
        public ConventionException(string message) : base(message)
        {
        }
    }
}
