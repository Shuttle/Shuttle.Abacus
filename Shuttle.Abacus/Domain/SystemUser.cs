using System;

namespace Shuttle.Abacus
{
    public class SystemUser
    {
        public SystemUser()
        {
            Construct();
        }

        public SystemUser(Guid id)
        {
            Construct();
        }

        public SystemUser(ICreateSystemUserCommand command)
        {
            Construct();

            LoginName = command.LoginName;

            command.PermissionIdentifiers.ForEach(identifier => Permissions.Add(new Permission(identifier)));
        }

        public IPermissionCollection Permissions { get; set; }

        public string LoginName { get; set; }

        private void Construct()
        {
            Permissions = new PermissionCollection();
        }

        public SystemUser ProcessCommand(ISetPermissionsCommand command)
        {
            Permissions = new PermissionCollection();

            command.PermissionIdentifiers.ForEach(identifier => Permissions.Add(new Permission(identifier)));

            return this;
        }

        public SystemUser ProcessCommand(IChangeLoginNameCommand command)
        {
            LoginName = command.NewLoginName;

            return this;
        }
    }
}
