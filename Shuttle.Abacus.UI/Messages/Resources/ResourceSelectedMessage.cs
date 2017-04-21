namespace Abacus.UI
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
