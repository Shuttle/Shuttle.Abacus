using System;

namespace Shuttle.Abacus.Domain
{
    public abstract class Constraint : IConstraint
    {
        protected Constraint(Guid argumentId, ValueType valueType)
        {
            ArgumentId = argumentId;
            ValueType = valueType;
        }

        public Guid ArgumentId { get; private set; }

        public ValueType ValueType { get; private set; }

        public abstract bool IsSatisfiedBy(IMethodContext collectionMethodContext);

        public abstract string Name { get; }
        public abstract string Text { get; }

        public abstract string Description();

        public abstract IConstraint Copy();
    }
}
