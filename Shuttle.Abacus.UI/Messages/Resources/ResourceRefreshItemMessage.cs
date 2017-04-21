namespace Abacus.UI
{
    public class ResourceRefreshItemMessage : NullPermissionMessage
    {
        public ResourceRefreshItemMessage(Resource item)
        {
            Item = item;
        }

        public Resource Item { get; private set; }
    }
}
