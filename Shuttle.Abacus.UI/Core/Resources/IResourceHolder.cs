namespace Shuttle.Abacus.Shell.Core.Resources
{
    public interface IResourceHolder
    {
        Resource Resource { get; }
        void AssignResource(Resource resource);
    }
}
