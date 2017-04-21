using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Policy;

namespace Shuttle.Abacus.ApplicationService
{
    public class ChangeLoginNameTask : IChangeLoginNameTask
    {
        private readonly ISystemUserPolicy systemUserPolicy;
        private readonly ISystemUserRepository systemUserRepository;

        public ChangeLoginNameTask(ISystemUserPolicy systemUserPolicy, ISystemUserRepository systemUserRepository)
        {
            this.systemUserPolicy = systemUserPolicy;
            this.systemUserRepository = systemUserRepository;
        }

        public void Execute(SystemUser user)
        {
            systemUserPolicy.InvariantRules().Enforce(user);

            systemUserRepository.ChangeLoginName(user);
        }
    }
}
