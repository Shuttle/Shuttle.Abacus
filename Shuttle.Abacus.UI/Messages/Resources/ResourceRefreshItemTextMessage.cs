using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Messages.Resources
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
