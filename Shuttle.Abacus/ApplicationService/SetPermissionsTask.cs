using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.ApplicationService
{
    public class SetPermissionsTask : ISetPermissionsTask
    {
        private readonly ISystemUserRepository systemUserRepository;

        public SetPermissionsTask(ISystemUserRepository systemUserRepository)
        {
            this.systemUserRepository = systemUserRepository;
        }

        public void Execute(SystemUser user)
        {
            systemUserRepository.SetPermissions(user);
        }
    }
}
