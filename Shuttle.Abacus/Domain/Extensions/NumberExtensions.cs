using System;

namespace Shuttle.Abacus
{
    public static class NumberExtensions
    {
        static public decimal RoundToCents(this decimal d)
        {
            return Math.Round(d, 2);
        }
    }
}