namespace Shuttle.Abacus.Domain
{
    public interface IFormulaCalculationContext : ICalculationContext
    {
        decimal FormulaTotal { get; }
        void ZeroFormulaTotal();
        void SetFormulaTotal(decimal value);
    }
}
