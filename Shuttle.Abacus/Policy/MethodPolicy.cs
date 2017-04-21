using System;
using Abacus.Domain;
using Abacus.Validation;

namespace Abacus.Policy
{
    public class MethodPolicy : AbstractPolicy<Method>, IMethodPolicy
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
