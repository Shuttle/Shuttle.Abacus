using System;

namespace Shuttle.Abacus.Domain
{
    public class ZeroCalculationResult : ICalculationResult
    {
        public ZeroCalculationResult(string name) 
        {
            Name = name;
        }

        public string Name { get; private set; }

        public decimal Value => decimal.Zero;

        public string Description()
        {
            return string.Format("{0}: zero", Name);
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
