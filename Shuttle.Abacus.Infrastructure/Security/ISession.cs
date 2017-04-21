namespace Shuttle.Abacus.Infrastructure
{
    public interface ISession
    {
        IPermissionCollection Permissions { get; set; }
    }
}
