namespace Shuttle.Abacus.UI.Core.Resources
{
    public interface IResourceHolder
    {
        Resource Resource { get; }
        void AssignResource(Resource resource);
    }
}
