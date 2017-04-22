using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.Resources
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
