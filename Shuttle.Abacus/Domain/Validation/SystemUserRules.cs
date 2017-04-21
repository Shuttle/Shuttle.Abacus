namespace Shuttle.Abacus
{
    public class SystemUserRules : ISystemUserRules
    {
        public IRuleCollection<object> LoginNameRules()
        {
            return Rule.With().Required().MaximumLength(100).Create();
        }
    }
}
