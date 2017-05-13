using System;

namespace Shuttle.Abacus.Domain
{
    public class ToConstraint : Constraint
    {
        public ToConstraint(Guid argumentId, ValueType valueType)
            : base(argumentId, valueType)
        {
        }

        public override string Name => "To";

        public override string Text => "To";

        public override bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var answer = collectionMethodContext.GetArgumentAnswer(ValueType.ArgumentName);

            return !answer.IsNull
                       ? answer.CompareTo(ValueType) <= 0
                       : false;
        }

        public override string Description()
        {
            return string.Format("'{0}' to '{1}'", ValueType.ArgumentName, ValueType.Description());
        }

        public override IConstraint Copy()
        {
            return new ToConstraint(ArgumentId, ValueType);
        }
    }
}
