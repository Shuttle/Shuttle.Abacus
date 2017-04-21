namespace Abacus.UI
{
    public class ResourceRefreshItemTextMessage : NullPermissionMessage
    {
        public Resource Item { get; private set; }

        public ResourceRefreshItemTextMessage(Resource item)
        {
            Item = item;
        }
    }
}
