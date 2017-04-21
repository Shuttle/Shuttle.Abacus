using System;
using Abacus.Domain;
using Abacus.Infrastructure;
using Abacus.Validation;

namespace Abacus.Data
{
    public class SystemUserRepository : Repository<SystemUser>, ISystemUserRepository
    {
        private readonly ISystemUserRules rules;
        private readonly IDataReaderRepository<SystemUser> repository;
        private readonly IDatabaseGateway gateway;

        public SystemUserRepository(ISystemUserRules rules, IDataReaderRepository<SystemUser> repository, IDatabaseGateway gateway)
        {
            this.rules = rules;
            this.repository = repository;
            this.gateway = gateway;
        }

        public SystemUser FetchByLoginName(string loginName)
        {
            return repository.FetchItemUsing(SystemUserTableAccess.Get(loginName));
        }

        public void SetPermissions(SystemUser user)
        {
            gateway.ExecuteUsing(SystemUserTableAccess.DeletePermissions(user));

            AddPermissions(user);
        }

        public void ChangeLoginName(SystemUser user)
        {
            gateway.ExecuteUsing(SystemUserTableAccess.ChangeLoginName(user));
        }

        public override void Add(SystemUser user)
        {
            ApplyInvariants(user);

            gateway.ExecuteUsing(SystemUserTableAccess.Add(user));

            AddPermissions(user);
        }

        private void AddPermissions(SystemUser user)
        {
            foreach (var permission in user.Permissions)
            {
                gateway.ExecuteUsing(SystemUserTableAccess.AddPermission(user, permission));
            }
        }

        public override void Remove(SystemUser user)
        {
            gateway.ExecuteUsing(SystemUserTableAccess.DeleteUser(user));
        }

        public void Save(SystemUser user)
        {
            ApplyInvariants(user);

            gateway.ExecuteUsing(SystemUserTableAccess.Update(user));
            gateway.ExecuteUsing(SystemUserTableAccess.DeletePermissions(user));

            AddPermissions(user);
        }

        public override SystemUser Get(Guid id)
        {
            var result = repository.FetchItemUsing(SystemUserTableAccess.Get(id));

            Guard.AgainstMissing<SystemUser>(result, id);

            return result;
        }

        private void ApplyInvariants(SystemUser user)
        {
            rules.LoginNameRules().Enforce(user.LoginName);
        }
    }
}
