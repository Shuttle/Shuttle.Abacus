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
            return new FormulaResultValueSource(_formulaRepository.Get(new Guid(value)));
        }
    }
}
