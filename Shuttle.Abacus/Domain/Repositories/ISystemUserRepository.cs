using System;

namespace Shuttle.Abacus.Domain
{
    public interface ISystemUserRepository
    {
        SystemUser FetchByLoginName(string loginName);
        void SetPermissions(SystemUser user);
        void ChangeLoginName(SystemUser user);
        void Add(SystemUser user);
        void Remove(Guid id);
        SystemUser Get(Guid id);
        void Save(SystemUser user);
    }
}
