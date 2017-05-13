using System;

namespace Shuttle.Abacus.Domain
{
    public class NotEqualToConstraint : Constraint
    {
        public NotEqualToConstraint(Guid argumentId, ValueType valueType)
            : base(argumentId, valueType)
        {
        }

        public override string Name => "NotEqualTo";

        public override string Text => "Not Equal To";

        public override bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var answer = collectionMethodContext.GetArgumentAnswer(ValueType.ArgumentName);

            return !answer.IsNull ? answer.CompareTo(ValueType) != 0 : false;
        }

        public override string Description()
        {
            return string.Format("'{0}' not equal to '{1}'", ValueType.ArgumentName, ValueType.Description());
        }

        public override IConstraint Copy()
        {
            return new EqualsConstraint(ArgumentId, ValueType);
        }
    }
}
