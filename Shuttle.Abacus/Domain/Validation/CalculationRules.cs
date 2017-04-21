namespace Shuttle.Abacus
{
    public class CalculationRules : ICalculationRules
    {
        public IRuleCollection<object> CalculationNameRules()
        {
            return Rule.With().Required().MaximumLength(50).Create();
        }

        public IRuleCollection<object> TypeRules()
        {
            return Rule.With().Required().Create();
        }
    }
}
