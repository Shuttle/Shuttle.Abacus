using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Policy;

namespace Shuttle.Abacus.ApplicationService
{
    public class CreateMethodTask : ICreateMethodTask
    {
        private readonly IMethodPolicy policy;
        private readonly IMethodRepository repository;

        public CreateMethodTask(IMethodPolicy policy, IMethodRepository repository)
        {
            this.policy = policy;
            this.repository = repository;
        }

        public void Execute(Method method)
        {
            policy.InvariantRules().Enforce(method);

            repository.Add(method);
        }
    }
}
