using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class FormulaSubTotalValueSourceFactory : IValueSourceFactory
    {
        private readonly IFormulaRepository _formulaRepository;

        public FormulaSubTotalValueSourceFactory(IFormulaRepository formulaRepository)
        {
            Guard.AgainstNull(formulaRepository, "formulaRepository");

            _formulaRepository = formulaRepository;
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
            return new FormulaSubTotalValueSource(_formulaRepository.Get(new Guid(value)));
        }
    }
}
