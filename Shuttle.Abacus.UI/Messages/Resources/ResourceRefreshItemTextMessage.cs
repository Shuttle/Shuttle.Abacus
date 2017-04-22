using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.Resources
{
    public class ResourceRefreshItemTextMessage : NullPermissionMessage
    {
        public Resource Item { get; private set; }

        public ResourceRefreshItemTextMessage(Resource item)
        {
            Item = item;
        }
    }
}
