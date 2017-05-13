using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class FormulaResultValueSourceFactory : IValueSourceFactory
    {
        private readonly IFormulaRepository _formulaRepository;

        public FormulaResultValueSourceFactory(IFormulaRepository formulaRepository)
        {
            Guard.AgainstNull(formulaRepository, "formulaRepository");

            _formulaRepository = formulaRepository;
        }

        public string Name => "CalculationResult";

        public string Text => "Calculation Result";

        public string Type => "Selection";

        public IValueSource Create(string value)
        {
            return new FormulaResultValueSource(_formulaRepository.Get(new Guid(value)));
        }
    }
}
