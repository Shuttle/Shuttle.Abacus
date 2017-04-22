using System;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class SystemUser
    {
        public Guid Id { get; private set; }

        public SystemUser()
        {
            Construct();
        }

        public SystemUser(Guid id)
        {
            Construct(id);
        }

        private void Construct(Guid id)
        {
            Id = id;
            Permissions = new PermissionCollection();
        }

        public SystemUser(CreateSystemUserCommand command)
        {
            Construct();

            LoginName = command.LoginName;

            command.PermissionIdentifiers.ForEach(identifier => Permissions.Add(new Permission(identifier)));
        }

        public IPermissionCollection Permissions { get; set; }

        public string LoginName { get; set; }

        private void Construct()
        {
            Construct(Guid.NewGuid());
        }

        public SystemUser ProcessCommand(SetPermissionsCommand command)
        {
            Permissions = new PermissionCollection();

            command.PermissionIdentifiers.ForEach(identifier => Permissions.Add(new Permission(identifier)));

            return this;
        }

        public SystemUser ProcessCommand(ChangeLoginNameCommand command)
        {
            LoginName = command.NewLoginName;

            return this;
        }
    }
}
