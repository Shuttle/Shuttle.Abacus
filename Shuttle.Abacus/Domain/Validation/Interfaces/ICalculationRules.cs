namespace Shuttle.Abacus.Domain
{
    public interface ICalculationRules
    {
        IRuleCollection<object> CalculationNameRules();
        IRuleCollection<object> TypeRules();
    }
}
