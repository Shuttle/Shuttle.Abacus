using Abacus.Domain;

namespace Abacus.Application
{
    public class SetCalculationConstraintsTask : ISetCalculationConstraintsTask
    {
        private readonly IConstraintRepository constraintRepository;

        public SetCalculationConstraintsTask(IConstraintRepository constraintRepository)
        {
            this.constraintRepository = constraintRepository;
        }

        public void Execute(Calculation item)
        {
            constraintRepository.SaveForOwner(item);
        }
    }
}
