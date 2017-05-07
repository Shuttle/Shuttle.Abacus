using Abacus.Domain;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Domain
{
    public class FormulaCalculationResult : AbstractCalculationResult
    {
        public FormulaCalculationResult(Formula formula, decimal value) : base(formula)
        {
            Value = value;
        }

        public override string Description()
        {
            return string.Format("{0}: {1}",
                                 Formula.Name,
                                 Value.ToString(Resources.FormatDecimal));
        }

        public override void Add(ICalculationResult result)
        {
            Value += result.Value;
        }

        public override void Limit(decimal value)
        {
            Value = value;
        }
    }
}
