using Shuttle.Abacus.UI.Core.Resources;
using Shuttle.Abacus.UI.Messages.Core;
using Shuttle.Abacus.UI.Navigation;

namespace Shuttle.Abacus.UI.Messages.Resources
{
    public class ResourceMenuRequestMessage : NullPermissionMessage
    {
        public ResourceMenuRequestMessage(Resource item, ResourceCollection relatedItems, ResourceCollection upstreamItems)
        {
            Item = item;
            RelatedItems = relatedItems;
            UpstreamItems = upstreamItems;
            NavigationItems = new NavigationItemCollection();
        }

        public Resource Item { get; private set; }
        public ResourceCollection RelatedItems { get; private set; }
        public NavigationItemCollection NavigationItems { get; private set; }
        public ResourceCollection UpstreamItems { get; private set; }
    }
}
