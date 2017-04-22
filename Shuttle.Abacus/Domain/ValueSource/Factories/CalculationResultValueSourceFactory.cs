using System;

namespace Shuttle.Abacus.Domain
{
    public class CalculationResultValueSourceFactory : IValueSourceFactory
    {
        private readonly ICalculationRepository _calculationRepository;

        public CalculationResultValueSourceFactory(ICalculationRepository calculationRepository)
        {
            _calculationRepository = calculationRepository;
        }

        public string Name
        {
            get { return "CalculationResult"; }
        }

        public string Text
        {
            get { return "Calculation Result"; }
        }

        public string Type
        {
            get { return "Selection"; }
        }

        public IValueSource Create(string value)
        {
            return new CalculationResultValueSource(_calculationRepository.Get(new Guid(value)));
        }
    }
}
