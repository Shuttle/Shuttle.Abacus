using System;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class SystemUser
    {
        public SystemUser()
            : this(Guid.NewGuid())
        {
        }

        public SystemUser(Guid id)
        {
            Id = id;
            Permissions = new PermissionCollection();
        }

        public Guid Id { get; }

        public IPermissionCollection Permissions { get; }

        public string LoginName { get; set; }

        public void OnAddPermission(string permission)
        {
            Permissions.Add(new Permission(permission));
        }
    }
}