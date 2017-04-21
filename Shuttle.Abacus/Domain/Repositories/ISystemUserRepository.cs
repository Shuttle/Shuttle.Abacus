namespace Shuttle.Abacus.Domain
{
    public interface ISystemUserRepository : IRepository<SystemUser>
    {
        SystemUser FetchByLoginName(string loginName);
        void SetPermissions(SystemUser user);
        void ChangeLoginName(SystemUser user);
    }
}
