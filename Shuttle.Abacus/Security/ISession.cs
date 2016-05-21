namespace Shuttle.Abacus
{
    public interface ISession
    {
        IPermissionCollection Permissions { get; set; }
    }
}
