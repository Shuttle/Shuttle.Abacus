using System;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Domain
{
    public class FormulaResultValueSource : IValueSource, ICalculationValueSource
    {
        private readonly Formula _formula;
        private Guid calculationId;

        public FormulaResultValueSource(Formula formula)
        {
            this._formula = formula;

            calculationId = formula.Id;
        }

        public string ValueSelection => calculationId.ToString("n");

        public void AssignCalculationId(Guid id)
        {
            calculationId = id;
        }

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return methodContext.GetResult(_formula.Name).Value;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (from formula result {1})", operand.ToString(Resources.FormatDecimal), _formula.Name);
        }

        public string Name => "CalculationResult";

        public object Text => _formula.Name;

        public IValueSource Copy()
        {
            return new FormulaResultValueSource(_formula);
        }
    }
}
