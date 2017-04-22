using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public class SystemUserQueryFactory : ISystemUserQueryFactory
    {
        private  readonly string SelectClausePermissions = @"
select {0}
    u.SystemUserID,
    u.LoginName,
    p.Permission
from
    SystemUser u
left join
    SystemUserPermission p on
        (p.SystemUserID = u.SystemUserID)
";

        public IQuery All()
        {
            return RawQuery.Create(@"
select
    SystemUserId,
    LoginName
order by    
    LoginName
");
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
    SystemUserId,
    LoginName
where
    SystemUserId = @SystemUserId
")
                .AddParameterValue(SystemUserColumns.Id, id);
        }

        public IQuery GetPermissions(Guid id)
        {
            return RawQuery.Create(@"
select
    Permission
from
    SystemUserPermission
where
    SystemUserId = @SystemUserId
")
                .AddParameterValue(PermissionColumns.SystemUserId, id);
        }

        public  IQuery FetchAll()
        {
            return FetchAll(0);
        }

        public  IQuery FetchAll(int top)
        {
            return RawQuery.Create(SelectClausePermissionsTop(top));
        }

        private  string SelectClausePermissionsTop(int top)
        {
            return string.Format(SelectClausePermissions,
                top < 1
                    ? string.Empty
                    : string.Concat("top ", top.ToString()));
        }

        public  IQuery FetchByLoginName(string loginName)
        {
            return RawQuery.Create(string.Concat(SelectClausePermissionsTop(0), "where u.LoginName = @LoginName"))
                .AddParameterValue(SystemUserColumns.LoginName, loginName);
        }

        public  IQuery FetchById(Guid id)
        {
            return RawQuery.Create(string.Concat(SelectClausePermissionsTop(0), "where u.SystemUserId = @SystemUserId"))
                .AddParameterValue(SystemUserColumns.Id, id);
        }

        public  IQuery Add(SystemUser user)
        {
            return RawQuery.Create(@"
insert into SystemUser
(
    SystemUserId,
    LoginName
)
values
(
    @SystemUserId,
    @LoginName
)")
                .AddParameterValue(SystemUserColumns.Id, user.Id)
                .AddParameterValue(SystemUserColumns.LoginName, user.LoginName);
        }

        public  IQuery Update(SystemUser user)
        {
            return RawQuery.Create("update SystemUser set LoginName = @LoginName where SystemUserId = @SystemUserId")
                .AddParameterValue(SystemUserColumns.LoginName, user.LoginName)
                .AddParameterValue(SystemUserColumns.Id, user.Id);
        }

        public  IQuery DeleteUser(SystemUser user)
        {
            return RawQuery.Create("delete from SystemUserSystemUserId = @SystemUserId")
                .AddParameterValue(SystemUserColumns.Id, user.Id);
        }

        public  IQuery AddPermission(SystemUser user, IPermission permission)
        {
            return RawQuery.Create(@"
insert into SystemUserPermission
(
    SystemUserId,
    Permission
)
values
(
    @SystemUserId,
    @Permission
)")
                .AddParameterValue(PermissionColumns.SystemUserId, user.Id)
                .AddParameterValue(PermissionColumns.Permission, permission.Identifier);
        }

        public  IQuery DeletePermissions(SystemUser user)
        {
            return RawQuery.Create("delete from SystemUserPermission where SystemUserId = @SystemUserId")
                .AddParameterValue(SystemUserColumns.Id, user.Id);
        }
    }
}