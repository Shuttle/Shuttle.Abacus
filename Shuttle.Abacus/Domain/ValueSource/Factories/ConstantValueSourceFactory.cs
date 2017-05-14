using System;

namespace Shuttle.Abacus.Domain
{
    public class ConstantValueSourceFactory : IValueSourceFactory
    {
        public string Name => "Constant";
        public string Text => "Constant";
        public string Type => "Fixed";

        public IValueSource Create(string value)
        {
            decimal dec;

            if (!decimal.TryParse(value, out dec))
            {
                throw new ArgumentException();
            }

            return new ConstantValueSource(dec);
        }
    }
}