using Abacus.Domain;

namespace Shuttle.Abacus.ApplicationService
{
    public class OwnerModel
    {
        public OwnerModel(IEntity owner, IEntity entity)
        {
            Owner = owner;
            Entity = entity;
        }

        public IEntity Owner { get; private set; }
        public IEntity Entity { get; private set; }
    }
}
