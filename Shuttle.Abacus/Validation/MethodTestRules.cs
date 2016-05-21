namespace Shuttle.Abacus
{
    public class MethodTestRules : IMethodTestRules
    {
        public IRuleCollection<object> DescriptionRules()
        {
            return Rule.With().Required().MaximumLength(250).Create();
        }

        public IRuleCollection<object> ExpectedResultRules()
        {
            return Rule.With().Required().MaximumLength(20).Decimal().Create();
        }
    }
}
