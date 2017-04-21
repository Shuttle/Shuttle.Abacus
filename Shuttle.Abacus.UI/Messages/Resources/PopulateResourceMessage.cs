namespace Abacus.UI
{
    public class PopulateResourceMessage : NullPermissionMessage
    {
        public PopulateResourceMessage(Resource resource, ResourceCollection relatedResources)
        {
            Resource = resource;
            RelatedResources = relatedResources;
            Resources = new ResourceCollection();
        }

        public Resource Resource { get; private set; }
        public ResourceCollection RelatedResources { get; private set; }
        public ResourceCollection Resources { get; private set; }

        public void Add(Resource resource)
        {
            Resources.Add(resource);
        }
    }
}
