using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;

namespace Shuttle.Abacus.Shell.Messages.Resources
{
    public class ResourceSelectedMessage : NullPermissionMessage
    {
        public ResourceSelectedMessage(Resource item, ResourceCollection relatedItems)
        {
            Item = item;
            RelatedItems = relatedItems;
        }

        public Resource Item { get; private set; }
        public ResourceCollection RelatedItems { get; private set; }
    }
}
