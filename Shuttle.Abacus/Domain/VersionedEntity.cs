using System;

namespace Abacus.Domain
{
    public abstract class VersionedEntity<TEntity> : Entity<TEntity> where TEntity : Entity<TEntity>
    {
        protected VersionedEntity()
        {
            Version = 1;
        }

        protected VersionedEntity(Guid id, int version) : base(id)
        {
            Version = version;
        }

        protected VersionedEntity(Guid id) : base(id)
        {
            Version = 1;
        }

        public virtual int Version { get; private set; }

        public void IncrementVersion()
        {
            Version++;
        }

        public void AssignVersion(int version)
        {
            Version = version;
        }
    }
}
