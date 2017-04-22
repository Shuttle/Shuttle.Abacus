using System;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Data;

namespace Shuttle.Abacus.DataAccess
{
    public interface ISystemUserQueryFactory
    {
        IQuery All();
        IQuery Get(Guid id);
        IQuery GetPermissions(Guid id);
        IQuery FetchAll();
        IQuery FetchAll(int top);
        IQuery FetchByLoginName(string loginName);
        IQuery FetchById(Guid id);
        IQuery Add(SystemUser user);
        IQuery Update(SystemUser user);
        IQuery DeleteUser(SystemUser user);
        IQuery AddPermission(SystemUser user, IPermission permission);
        IQuery DeletePermissions(SystemUser user);
        IQuery Get(string loginName);
    }
}