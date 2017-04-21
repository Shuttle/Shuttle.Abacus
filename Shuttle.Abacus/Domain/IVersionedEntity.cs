namespace Abacus.Domain
{
    public interface IVersionedEntity
    {
        int Version { get; }

        void IncrementVersion();

        void AssignVersion(int version);
    }
}
