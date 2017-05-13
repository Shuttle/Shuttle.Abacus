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

        public string Name => "CalculationSubTotal";

        public string Text => "Calculation Sub-Total";

        public string Type => "Selection";

        public IValueSource Create(string value)
        {
            return new FormulaSubTotalValueSource(_formulaRepository.Get(new Guid(value)));
        }
    }
}
