using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Domain
{
    public class FormulaTotalValueSource : IValueSource
    {
        public decimal Operand(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            return ((IFormulaCalculationContext)calculationContext).FormulaTotal;
        }

        public string Description(decimal operand, IMethodContext methodContext)
        {
            return string.Format("{0} (formula total)", operand.ToString(Resources.FormatDecimal));
        }

        public string Name => "FormulaTotal";

        public object Text => string.Empty;

        public IValueSource Copy()
        {
            return new FormulaTotalValueSource();
        }
    }
}
