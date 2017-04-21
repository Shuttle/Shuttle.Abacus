using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Invariants
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
