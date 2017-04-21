using System;

namespace Shuttle.Abacus.Domain
{
    public class DecimalValueSourceFactory : IValueSourceFactory
    {
        public string Name
        {
            get { return "Decimal"; }
        }

        public string Text
        {
            get { return "Decimal"; }
        }

        public string Type
        {
            get { return "Fixed"; }
        }

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
