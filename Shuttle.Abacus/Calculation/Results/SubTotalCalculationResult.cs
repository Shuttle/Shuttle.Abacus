using System;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus
{
    public class SubTotalCalculationResult : ICalculationResult
    {
        public SubTotalCalculationResult(string name, decimal value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public decimal Value { get; private set; }

        public string Description()
        {
            return string.Format("{0}: {1}",
                                 Name,
                                 Value.ToString(Resources.FormatDecimal));
        }

        public void Add(ICalculationResult result)
        {
            throw new NotImplementedException();
        }

        public void Limit(decimal value)
        {
            throw new NotImplementedException();
        }
    }
}
