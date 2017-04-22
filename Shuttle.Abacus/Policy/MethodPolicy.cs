using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Invariants.Interfaces;

namespace Shuttle.Abacus.Policy
{
    public class MethodPolicy : IMethodPolicy
    {
        private readonly IMethodRules methodRules;

        public MethodPolicy(IMethodRules methodRules)
        {
            this.methodRules = methodRules;
        }

        public IRuleCollection<Method> InvariantRules()
        {
            return new RuleCollection<Method>
                (
                IdRule(), MethodNameRule()
                );
        }

        private static IRule<Method> IdRule()
        {
            return
                new Rule<Method>
                    (
                    "Id may not be empty.",
                    (item, rule) => item.Id.Equals(Guid.Empty));
        }

        private IRule<Method> MethodNameRule()
        {
            return
                new Rule<Method>
                    (
                    "Method name errors:",
                    (item, rule) =>
                        {
                            var result = methodRules.MethodNameRules().BrokenBy(item.MethodName);

                            if (result.IsEmpty)
                            {
                                return false;
                            }

                            rule.Message.AddDetailMessages(result.Messages);

                            return true;
                        });
        }
    }
}
