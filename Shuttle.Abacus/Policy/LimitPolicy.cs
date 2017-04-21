using Abacus.Domain;
using Abacus.Validation;

namespace Abacus.Policy
{
    public class LimitPolicy : AbstractPolicy<Limit>, ILimitPolicy
    {
        private readonly ILimitRules limitRules;

        public LimitPolicy(ILimitRules limitRules)
        {
            this.limitRules = limitRules;
        }

        public IRuleCollection<Limit> InvariantRules()
        {
            return new RuleCollection<Limit>
                (
                LimitNameRule(), TypeRule()
                );
        }

        private IRule<Limit> LimitNameRule()
        {
            return
                new Rule<Limit>
                    (
                    "Limit name errors:",
                    (item, rule) =>
                        {
                            var result = limitRules.LimitNameRules().BrokenBy(item.Name);

                            if (result.IsEmpty)
                            {
                                return false;
                            }

                            rule.Message.AddDetailMessages(result.Messages);

                            return true;
                        });
        }

        private IRule<Limit> TypeRule()
        {
            return
                new Rule<Limit>
                    (
                    "Limit type errors:",
                    (item, rule) =>
                        {
                            var result = limitRules.TypeRules().BrokenBy(item.Name);

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
