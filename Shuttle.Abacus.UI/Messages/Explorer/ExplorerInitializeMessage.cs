using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Messages.Explorer
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
