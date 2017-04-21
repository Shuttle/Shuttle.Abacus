using Abacus.Domain;
using Abacus.Policy;

namespace Abacus.Application
{
    public class CreateFormulaTask : ICreateFormulaTask
    {
        private readonly IFormulaPolicy policy;
        private readonly IFormulaRepository repository;

        public CreateFormulaTask(IFormulaPolicy policy, IFormulaRepository repository)
        {
            this.policy = policy;
            this.repository = repository;
        }

        public void Execute(OwnerModel model)
        {
            policy.InvariantRules().Enforce((Formula) model.Entity);

            repository.Add((IFormulaOwner) model.Owner, (Formula) model.Entity);
        }
    }
}
