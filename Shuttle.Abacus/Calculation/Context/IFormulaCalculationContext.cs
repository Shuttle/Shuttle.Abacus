namespace Shuttle.Abacus
{
    public interface IFormulaCalculationContext : ICalculationContext
    {
        decimal FormulaTotal { get; }
        void ZeroFormulaTotal();
        void SetFormulaTotal(decimal value);
    }
}
