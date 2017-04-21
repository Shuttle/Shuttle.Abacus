using System;
using System.Data;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DataAccess.Definitions
{
    public static class SystemUserTableAccess
    {
        public const string PermissionTableName = "SystemUserPermission";
        

        public IQuery All()
        {
            return RawQuery.Create(@"
select
                Id,
                LoginName,
                .OrderBy(Columns.LoginName).Ascending()
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

        private static IQuery FetchAll()
        {
            return FetchAll(null);
        }

        private static IQuery FetchAll(int? top)
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
                !top.HasValue ? string.Empty : top.Value.ToString());
        }

        public static IQuery Get(string loginName)
        {
            return
                DynamicQuery.CreateFrom
                    (
                    FetchAll().Build() +
                    WhereBuilder.AddParameterValue(Columns.LoginName).WithEqualTo().Build()
                    )
                    .AddParameterValue(Columns.LoginName, loginName);
        }

        public static IQuery Get(Guid id)
        {
            return
                DynamicQuery.CreateFrom
                    (
                    FetchAll().Build() +
                    WhereBuilder.AddParameterValue(Columns.IdAliased, id).Build()
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
                .AddParameterValue(Columns.Id).HasValue(user.Id);
        }

        public static IQuery DeleteUser(SystemUser user)
        {
            return DeleteBuilder.AddParameterValue(Columns.Id, user.Id).From(TableName);
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
            return DeleteBuilder.AddParameterValue(Columns.Id, user.Id).From(PermissionTableName);
        }

        public static IQuery ChangeLoginName(SystemUser user)
        {
            return
                UpdateBuilder
                    .Update(TableName)
                    .Set(Columns.LoginName).ToValue(user.LoginName)
                    .AddParameterValue(Columns.Id).HasValue(user.Id);
        }

        public static class Columns
        {
            public static readonly MappedColumn<Guid> Id =
                new MappedColumn<Guid>("SystemUserID", DbType.Guid);

            public static readonly MappedColumn<Guid> IdAliased =
                new MappedColumn<Guid>("u.SystemUserID", DbType.Guid);

            public static readonly MappedColumn<string> LoginName = new MappedColumn<string>("LoginName",
                                                                                           DbType.AnsiString, 100);
        }

        public static class PermissionColumns
        {
            public static readonly MappedColumn<string> Permission =
                new MappedColumn<string>("Permission", DbType.AnsiString, 100);

            public static readonly MappedColumn<Guid> SystemUserId =
                new MappedColumn<Guid>("SystemUserID", DbType.Guid);
        }
    }
}
