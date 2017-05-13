using System;

namespace Shuttle.Abacus.Domain
{
    public class EqualsConstraint : Constraint
    {
        public EqualsConstraint(Guid argumentId, ValueType valueType)
            : base(argumentId, valueType)
        {
        }

        public override string Name => "Equals";

        public override string Text => "Equals";

        public override bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var answer = collectionMethodContext.GetArgumentAnswer(ValueType.ArgumentName);

            return !answer.IsNull ? answer.CompareTo(ValueType) == 0 : false;
        }

        public override string Description()
        {
            return string.Format("'{0}' equals '{1}'", ValueType.ArgumentName, ValueType.Description());
        }

        public override IConstraint Copy()
        {
            return new EqualsConstraint(ArgumentId, ValueType);
        }
    }
}
