using System;
using System.Data;
using Abacus.Domain;
using Abacus.Infrastructure;

namespace Abacus.Data
{
    public static class SystemUserTableAccess
    {
        public const string PermissionTableName = "SystemUserPermission";
        public const string TableName = "SystemUser";

        public static ISelectQuery All()
        {
            return SelectBuilder
                .Select(Columns.Id)
                .With(Columns.LoginName)
                .OrderBy(Columns.LoginName).Ascending()
                .From(TableName);
        }

        public static ISelectQuery GetPermissions(Guid id)
        {
            return SelectBuilder
                .Select(PermissionColumns.Permission)
                .Where(PermissionColumns.SystemUserId).EqualTo(id)
                .From(PermissionTableName);
        }

        private static IQuery FetchAll()
        {
            return FetchAll(null);
        }

        private static IQuery FetchAll(int? top)
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
                !top.HasValue ? string.Empty : top.Value.ToString());
        }

        public static IQuery Get(string loginName)
        {
            return
                DynamicQuery.CreateFrom
                    (
                    FetchAll().Build() +
                    WhereBuilder.Where(Columns.LoginName).WithEqualTo().Build()
                    )
                    .AddParameterValue(Columns.LoginName, loginName);
        }

        public static IQuery Get(Guid id)
        {
            return
                DynamicQuery.CreateFrom
                    (
                    FetchAll().Build() +
                    WhereBuilder.Where(Columns.IdAliased).EqualTo(id).Build()
                    )
                    .AddParameterValue(Columns.IdAliased, id);
        }

        public static IQuery Add(SystemUser user)
        {
            return InsertBuilder.Insert()
                .Add(Columns.Id).WithValue(user.Id)
                .Add(Columns.LoginName).WithValue(user.LoginName)
                .Into(TableName);
        }

        public static IQuery Update(SystemUser user)
        {
            return UpdateBuilder.Update(TableName)
                .Set(Columns.LoginName).ToValue(user.LoginName)
                .Where(Columns.Id).HasValue(user.Id);
        }

        public static IQuery DeleteUser(SystemUser user)
        {
            return DeleteBuilder.Where(Columns.Id).EqualTo(user.Id).From(TableName);
        }

        public static IQuery AddPermission(SystemUser user, IPermission permission)
        {
            return InsertBuilder.Insert()
                .Add(PermissionColumns.SystemUserId).WithValue(user.Id)
                .Add(PermissionColumns.Permission).WithValue(permission.Identifier)
                .Into(PermissionTableName);
        }

        public static IQuery DeletePermissions(SystemUser user)
        {
            return DeleteBuilder.Where(Columns.Id).EqualTo(user.Id).From(PermissionTableName);
        }

        public static IQuery ChangeLoginName(SystemUser user)
        {
            return
                UpdateBuilder
                    .Update(TableName)
                    .Set(Columns.LoginName).ToValue(user.LoginName)
                    .Where(Columns.Id).HasValue(user.Id);
        }

        public static class Columns
        {
            public static readonly QueryColumn<Guid> Id =
                new QueryColumn<Guid>("SystemUserID", DbType.Guid).AsIdentifier();

            public static readonly QueryColumn<Guid> IdAliased =
                new QueryColumn<Guid>("u.SystemUserID", DbType.Guid).AsIdentifier();

            public static readonly QueryColumn<string> LoginName = new QueryColumn<string>("LoginName",
                                                                                           DbType.AnsiString, 100);
        }

        public static class PermissionColumns
        {
            public static readonly QueryColumn<string> Permission =
                new QueryColumn<string>("Permission", DbType.AnsiString, 100);

            public static readonly QueryColumn<Guid> SystemUserId =
                new QueryColumn<Guid>("SystemUserID", DbType.Guid);
        }
    }
}
