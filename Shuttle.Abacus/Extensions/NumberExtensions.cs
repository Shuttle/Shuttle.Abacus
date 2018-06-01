using System;

namespace Shuttle.Abacus.Extensions
{
    public static class NumberExtensions
    {
        public static decimal RoundToCents(this decimal d)
        {
            return Math.Round(d, 2);
        }
    }
}