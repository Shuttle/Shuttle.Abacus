using System;

namespace Abacus.Domain
{
    public abstract class Entity<TEntity> : IEntity where TEntity : Entity<TEntity>
    {
        private int? oldHashCode;

        protected Entity()
        {
            AssignId();
        }

        protected Entity(Guid id)
        {
            AssignId(id);
        }

        public Guid Id { get; private set; }

        public void AssignId()
        {
            Id = Guid.NewGuid();
        }

        public void AssignId(Guid id)
        {
            Id = id;
        }

        public bool Equals(Entity<TEntity> other)
        {
            return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) || Equals(other.Id, Id));
        }

        public override int GetHashCode()
        {
            if (oldHashCode.HasValue)
            {
                return oldHashCode.Value;
            }

            var thisIsTransient = Equals(Id, Guid.Empty);

            if (thisIsTransient)
            {
                oldHashCode = base.GetHashCode();

                return oldHashCode.Value;
            }

            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as TEntity;

            if (other == null)
            {
                return false;
            }

            var otherIsTransient = Equals(other.Id, Guid.Empty);
            var thisIsTransient = Equals(Id, Guid.Empty);

            return otherIsTransient && thisIsTransient ? ReferenceEquals(other, this) : other.Id.Equals(Id);
        }
    }
}
