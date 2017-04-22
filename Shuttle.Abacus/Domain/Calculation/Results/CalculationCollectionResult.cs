using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Domain
{
    public class CalculationCollectionResult : AbstractCalculationResult
    {
        public CalculationCollectionResult(Calculation calculation, decimal value) : base(calculation)
        {
            Value = value;
        }

        public CalculationCollectionResult(Calculation calculation)
            : this(calculation, 0)
        {
        }

        public override string Description()
        {
            return string.Format("{0}: {1}",
                                 Calculation.Name,
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
