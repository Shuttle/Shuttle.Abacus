using System;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class SystemUserQueries
    {
        public const string PermissionTableName = "SystemUserPermission";
        

        public IQuery All()
        {
            return RawQuery.Create(@"
select
                Id,
                LoginName,
                .OrderBy(SystemUserColumns.LoginName).Ascending()
                .From(TableName);
        }

        public IQuery Get(Guid id)
        {
            return RawQuery.Create(@"
select
                Id,
                LoginName,
                .AddParameterValue(SystemUserColumns.Id, id)
                .From(TableName);
        }

        public IQuery GetPermissions(Guid id)
        {
            return RawQuery.Create(@"
select
                Permission,
                .AddParameterValue(PermissionColumns.SystemUserId, id)
                .From(PermissionTableName);
        }

        public static IQuery FetchAll()
        {
            return FetchAll(null);
        }

        public static IQuery FetchAll(int? top)
        {
            return RawQuery.Create(
                @"
                    select {0}
                        u.SystemUserID,
                        u.LoginName,
                        p.Permission
                    from
                        SystemUser u
                    left join
                        SystemUserPermission p on
                            (p.SystemUserID = u.SystemUserID)
                ",
                !top.HasValue
                    ? string.Empty
                    : top.Value.ToString());
        }

        public static IQuery FetchByLoginName(string loginName)
        {
            return
                DynamicQuery.CreateFrom
                    (
                    FetchAll().Build() +
                    WhereBuilder.AddParameterValue(SystemUserColumns.LoginName, loginName).Build()
                    );
            //todo: remove .AddParameterValue(SystemUserColumns.LoginName, loginName);
        }

        public static IQuery FetchById(Guid id)
        {
            return
                DynamicQuery.CreateFrom
                    (
                    FetchAll().Build() +
                    WhereBuilder.AddParameterValue(SystemUserColumns.IdAliased, id).Build()
                    )
                    .AddParameterValue(SystemUserColumns.IdAliased, id);
        }
    }
}
