using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Policy;

namespace Shuttle.Abacus.ApplicationService
{
    public class ChangeMethodTask : IChangeMethodTask
    {
        private readonly IMethodPolicy policy;
        private readonly IMethodRepository repository;

        public ChangeMethodTask(IMethodPolicy policy, IMethodRepository repository)
        {
            this.policy = policy;
            this.repository = repository;
        }

        public void Execute(Method method)
        {
            policy.InvariantRules().Enforce(method);

            repository.Save(method);
        }
    }
}
