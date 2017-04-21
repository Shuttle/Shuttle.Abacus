namespace Shuttle.Abacus.Domain
{
    public class MethodRules : IMethodRules
    {
        public IRuleCollection<object> MethodNameRules()
        {
            return Rule.With().Required().MaximumLength(50).Create();
        }
    }
}
