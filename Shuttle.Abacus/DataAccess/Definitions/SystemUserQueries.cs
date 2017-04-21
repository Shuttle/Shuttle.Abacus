using System;

namespace Abacus.Data
{
    public static class SystemUserQueries
    {
        public const string PermissionTableName = "SystemUserPermission";
        public const string TableName = "SystemUser";

        public static ISelectQuery All()
        {
            return SelectBuilder
                .Select(SystemUserColumns.Id)
                .With(SystemUserColumns.LoginName)
                .OrderBy(SystemUserColumns.LoginName).Ascending()
                .From(TableName);
        }

        public static ISelectQuery Get(Guid id)
        {
            return SelectBuilder
                .Select(SystemUserColumns.Id)
                .With(SystemUserColumns.LoginName)
                .Where(SystemUserColumns.Id).EqualTo(id)
                .From(TableName);
        }

        public static ISelectQuery GetPermissions(Guid id)
        {
            return SelectBuilder
                .Select(PermissionColumns.Permission)
                .Where(PermissionColumns.SystemUserId).EqualTo(id)
                .From(PermissionTableName);
        }

        public static IQuery FetchAll()
        {
            return FetchAll(null);
        }

        public static IQuery FetchAll(int? top)
        {
            return DynamicQuery.CreateFrom(
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
                    WhereBuilder.Where(SystemUserColumns.LoginName).EqualTo(loginName).Build()
                    );
            //todo: remove .AddParameterValue(SystemUserColumns.LoginName, loginName);
        }

        public static IQuery FetchById(Guid id)
        {
            return
                DynamicQuery.CreateFrom
                    (
                    FetchAll().Build() +
                    WhereBuilder.Where(SystemUserColumns.IdAliased).EqualTo(id).Build()
                    )
                    .AddParameterValue(SystemUserColumns.IdAliased, id);
        }
    }
}
