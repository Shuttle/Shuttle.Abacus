using System;

namespace Shuttle.Abacus.Infrastructure
{
    public class InvalidCalculationOrderException : Exception
    {
        public InvalidCalculationOrderException(string message) : base(message)
        {
        }
    }
}
