using System;
using System.Data;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Interfaces;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class SystemUserRepository : Repository<SystemUser>, ISystemUserRepository
    {
        private readonly IDatabaseGateway _databaseGateway;
        private readonly ISystemUserRules _rules;
        private readonly ISystemUserQueryFactory _systemUserQueryFactory;
        private readonly IDataRowMapper<SystemUser> _mapper;

        public SystemUserRepository(IDatabaseGateway databaseGateway, ISystemUserQueryFactory systemUserQueryFactory, IDataRowMapper<SystemUser> mapper, ISystemUserRules rules)
        {
            _rules = rules;
            _databaseGateway = databaseGateway;
            _systemUserQueryFactory = systemUserQueryFactory;
            _mapper = mapper;
        }

        public SystemUser FetchByLoginName(string loginName)
        {
            var row = _databaseGateway.GetSingleRowUsing(_systemUserQueryFactory.Get(loginName));

            if (row == null)
            {
                return null;
            }

            return MapSystemUser(row);
        }

        private SystemUser MapSystemUser(DataRow row)
        {
            var result = _mapper.Map(row).Result;

            foreach (var permissionRow in _databaseGateway.GetRowsUsing(_systemUserQueryFactory.GetPermissions(result.Id)))
            {
                result.OnAddPermission(PermissionColumns.Permission.MapFrom(permissionRow));
            }

            return result;
        }

        public void SetPermissions(SystemUser user)
        {
            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.DeletePermissions(user));

            AddPermissions(user);
        }

        public void ChangeLoginName(SystemUser user)
        {
            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.Save(user));
        }

        public override void Add(SystemUser user)
        {
            ApplyInvariants(user);

            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.Add(user));

            AddPermissions(user);
        }

        public override void Remove(Guid id)
        {
            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.Remove(id));
        }

        public override SystemUser Get(Guid id)
        {
            var row = _databaseGateway.GetSingleRowUsing(_systemUserQueryFactory.Get(id));

            if (row == null)
            {
                throw Exceptions.MissingEntity("SystemUser", id);
            }

            return MapSystemUser(row);
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

            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.Save(user));
            _databaseGateway.ExecuteUsing(_systemUserQueryFactory.DeletePermissions(user));

            AddPermissions(user);
        }

        private void ApplyInvariants(SystemUser user)
        {
            _rules.LoginNameRules().Enforce(user.LoginName);
        }
    }
}