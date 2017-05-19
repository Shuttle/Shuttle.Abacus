using Shuttle.Abacus.Shell.Core.Resources;
using Shuttle.Abacus.Shell.Messages.Core;
using Shuttle.Abacus.Shell.Navigation;

namespace Shuttle.Abacus.Shell.Messages.Resources
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
