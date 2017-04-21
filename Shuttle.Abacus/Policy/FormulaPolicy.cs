using System;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Policy
{
    public class FormulaPolicy : AbstractPolicy<Formula>, IFormulaPolicy
    {
        public IRuleCollection<Formula> InvariantRules()
        {
            return new RuleCollection<Formula>
                (
                IdRule(), OperationsRule()
                );
        }

        private static IRule<Formula> OperationsRule()
        {
            return
                new Rule<Formula>
                    (
                    "The formula must contain at least one operation.",
                    (item, rule) => !item.HasOperations);
        }
    }
}
