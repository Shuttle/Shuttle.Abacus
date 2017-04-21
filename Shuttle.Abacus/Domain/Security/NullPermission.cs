namespace Shuttle.Abacus
{
    public class NullPermission : IPermission
    {
        public NullPermission()
        {
            Identifier = string.Empty;
            Description = string.Empty;
        }

        public string Identifier { get; private set; }
        public string Description { get; set; }
        
        public bool IsSatisfiedBy(IPermissionCollection item)
        {
            return true;
        }
    }
}
