namespace Abacus.UI
{
    public interface IResourceHolder
    {
        Resource Resource { get; }
        void AssignResource(Resource resource);
    }
}
