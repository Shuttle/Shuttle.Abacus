using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.Explorer
{
    public class ExplorerInitializeMessage : NullPermissionMessage
    {
        public ExplorerInitializeMessage()
        {
            Items = new ResourceCollection();
        }

        public ResourceCollection Items { get; private set; }
    }
}
