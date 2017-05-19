using Shuttle.Abacus.Shell.Core.Resources;

namespace Shuttle.Abacus.Shell.Messages.Core
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
