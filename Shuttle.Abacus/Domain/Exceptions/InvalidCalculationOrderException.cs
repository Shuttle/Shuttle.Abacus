using System;

namespace Shuttle.Abacus
{
    public class InvalidCalculationOrderException : Exception
    {
        public InvalidCalculationOrderException(string message) : base(message)
        {
        }
    }
}
