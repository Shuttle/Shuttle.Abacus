using System;

namespace Shuttle.Abacus.Domain
{
    public class DecimalValueSourceFactory : IValueSourceFactory
    {
        public string Name => "Decimal";

        public string Text => "Decimal";

        public string Type => "Fixed";

        public IValueSource Create(string value)
        {
            decimal dec;

            if (!decimal.TryParse(value, out dec))
            {
                throw new ArgumentException();
            }

            return new DecimalValueSource(dec);
        }
    }
}
