using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;

namespace Shuttle.Abacus.UI.Messages.Resources
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
