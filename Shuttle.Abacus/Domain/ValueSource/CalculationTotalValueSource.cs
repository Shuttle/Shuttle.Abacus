using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Domain
{
    public class CalculationTotalValueSource : IValueSource
    {
        public static CalculationTotalValueSource Instance = new CalculationTotalValueSource();

        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return methodContext.Total.Value;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (current running total)", operand.ToString(Resources.FormatDecimal));
        }

        public string Name => "CalculationTotal";

        public object Text => string.Empty;

        public IValueSource Copy()
        {
            return new CalculationTotalValueSource();
        }
    }
}
