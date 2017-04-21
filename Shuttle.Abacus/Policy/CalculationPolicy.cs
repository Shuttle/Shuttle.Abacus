using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Policy
{
    public class CalculationPolicy : ICalculationPolicy
    {
        private readonly ICalculationRules calculationRules;

        public CalculationPolicy(ICalculationRules calculationRules)
        {
            this.calculationRules = calculationRules;
        }

        public IRuleCollection<Calculation> InvariantRules()
        {
            return new RuleCollection<Calculation>
                (
                CalculationNameRule() 
                );
        }

        private IRule<Calculation> CalculationNameRule()
        {
            return
                new Rule<Calculation>
                    (
                    "Calculation name errors:",
                    (item, rule) =>
                        {
                            var result = calculationRules.CalculationNameRules().BrokenBy(item.Name);

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
