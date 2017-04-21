namespace Shuttle.Abacus
{
    public interface ICalculationRules
    {
        IRuleCollection<object> CalculationNameRules();
        IRuleCollection<object> TypeRules();
    }
}
