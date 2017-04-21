namespace Abacus.Validation
{
    public interface ICalculationRules
    {
        IRuleCollection<object> CalculationNameRules();
        IRuleCollection<object> TypeRules();
    }
}
