using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Policy;

namespace Shuttle.Abacus.ApplicationService
{
    public class ChangeFormulaTask : IChangeFormulaTask
    {
        private readonly IFormulaPolicy policy;
        private readonly IFormulaRepository repository;

        public ChangeFormulaTask(IFormulaPolicy policy, IFormulaRepository repository)
        {
            this.policy = policy;
            this.repository = repository;
        }

        public void Execute(Formula formula)
        {
            policy.InvariantRules().Enforce(formula);

            repository.Save(formula);
        }
    }
}
