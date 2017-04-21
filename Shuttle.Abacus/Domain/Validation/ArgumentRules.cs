namespace Shuttle.Abacus.Domain
{
    public class ArgumentRules : IArgumentRules
    {
        public IRuleCollection<object> ArgumentNameRules()
        {
            return Rule.With().Required().MaximumLength(100).Create();
        }

        public IRuleCollection<object> AnswerTypeRules()
        {
            return Rule.With().Required().Create();
        }

    }
}
