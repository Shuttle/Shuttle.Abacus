namespace Shuttle.Abacus
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
