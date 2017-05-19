using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Messages.Resources
{
    public class ResourceRefreshItemMessage : NullPermissionMessage
    {
        public ResourceRefreshItemMessage(Resource item)
        {
            Item = item;
        }

        public Resource Item { get; private set; }
    }
}
