using System;

namespace Shuttle.Abacus.Infrastructure
{
    public static class EnumerationExtensions
    {
        public static int AsID(this Enum enumeration)
        {
            return Convert.ToInt32(enumeration);
        }
    }
}
