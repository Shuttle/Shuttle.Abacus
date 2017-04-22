using Shuttle.Abacus.UI.Core.Resources;

namespace Shuttle.Abacus.UI.Messages.Core
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
