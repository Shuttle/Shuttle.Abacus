namespace Abacus.UI
{
    public abstract class ResourceMessage : NullPermissionMessage, IResourceHolder
    {
        public Resource Resource { get; private set; }

        public void AssignResource(Resource resource)
        {
            Resource = resource;
        }
    }
}
