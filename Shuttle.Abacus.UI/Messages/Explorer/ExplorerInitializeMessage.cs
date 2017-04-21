using Abacus.Infrastructure;

namespace Abacus.UI
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
