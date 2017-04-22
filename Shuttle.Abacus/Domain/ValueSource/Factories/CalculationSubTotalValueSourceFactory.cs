using System;

namespace Shuttle.Abacus.Domain
{
    public class CalculationSubTotalValueSourceFactory : IValueSourceFactory
    {
        private readonly ICalculationRepository _calculationRepository;

        public CalculationSubTotalValueSourceFactory(ICalculationRepository calculationRepository)
        {
            _calculationRepository = calculationRepository;
        }

        public string Name
        {
            get { return "CalculationSubTotal"; }
        }

        public string Text
        {
            get { return "Calculation Sub-Total"; }
        }

        public string Type
        {
            get { return "Selection"; }
        }

        public IValueSource Create(string value)
        {
            return new CalculationSubTotalValueSource(_calculationRepository.Get(new Guid(value)));
        }
    }
}
