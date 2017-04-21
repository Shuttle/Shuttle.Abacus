using System;

namespace Shuttle.Abacus.Domain
{
    public static class NumberExtensions
    {
        static public decimal RoundToCents(this decimal d)
        {
            return Math.Round(d, 2);
        }
    }
}