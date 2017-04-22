using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class SystemUserRepository : Repository<SystemUser>, ISystemUserRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly IDataRepository<SystemUser> _repository;
        private readonly ISystemUserRules _rules;
        private readonly ISystemUserQueryFactory _systemUserQueryFactory;

        public SystemUserRepository(IDatabaseGateway databaseGateway, ISystemUserQueryFactory systemUserQueryFactory,
            IDataRepository<SystemUser> repository, ISystemUserRules rules)
        {
            _rules = rules;
            _repository = repository;
            _databaseGateway = databaseGateway;
            _systemUserQueryFactory = systemUserQueryFactory;
        }

        public SystemUser FetchByLoginName(string loginName)
        {
            return _repository.FetchItemUsing(_systemUserQueryFactory.Get(loginName));
        }

        public void SetPermissions(SystemUser user)
        {
            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.DeletePermissions(user));

            AddPermissions(user);
        }

        public void ChangeLoginName(SystemUser user)
        {
            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.Update(user));
        }

        public override void Add(SystemUser user)
        {
            ApplyInvariants(user);

            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.Add(user));

            AddPermissions(user);
        }

        public override void Remove(SystemUser user)
        {
            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.DeleteUser(user));
        }

        public override SystemUser Get(Guid id)
        {
            var result = _repository.FetchItemUsing(_systemUserQueryFactory.Get(id));

            if (result == null)
            {
                throw Exceptions.MissingEntity("SystemUser", id);
            }

            return result;
        }

        private void AddPermissions(SystemUser user)
        {
            foreach (var permission in user.Permissions)
            {
                _databaseGateway.ExecuteUsing(_systemUserQueryFactory.AddPermission(user, permission));
            }
        }

        public void Save(SystemUser user)
        {
            ApplyInvariants(user);

            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.Update(user));
            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.DeletePermissions(user));

            AddPermissions(user);
        }

        private void ApplyInvariants(SystemUser user)
        {
            _rules.LoginNameRules().Enforce(user.LoginName);
        }
    }
}