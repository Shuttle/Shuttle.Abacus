using System;
using Shuttle.Abacus.Localisation;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class FormulaSubTotalValueSource : IValueSource, ICalculationValueSource
    {
        private readonly Formula _formula;
        private Guid _formulaId;

        public FormulaSubTotalValueSource(Formula formula)
        {
            Guard.AgainstNull(formula, "formula");

            _formula = formula;
            _formulaId = formula.Id;
        }

        public string ValueSelection => _formulaId.ToString("n");

        public void AssignCalculationId(Guid id)
        {
            _formulaId = id;
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return methodContext.GetSubTotal(_formula.Name).Value;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (from sub-total '{1}')", operand.ToString(Resources.FormatDecimal), _formula.Name);
        }

        public string Name => "CalculationSubTotal";

        public object Text => _formula.Name;

        public IValueSource Copy()
        {
            return new FormulaSubTotalValueSource(_formula);
        }
    }
}