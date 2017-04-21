namespace Shuttle.Abacus.Infrastructure
{
    public class Session : ISession
    {
        public Session()
        {
            Permissions = new PermissionCollection();
        }

        public IPermissionCollection Permissions { get; set; }
    }
}
