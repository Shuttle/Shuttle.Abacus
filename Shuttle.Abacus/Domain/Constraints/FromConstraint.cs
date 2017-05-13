using System;

namespace Shuttle.Abacus.Domain
{
    public class FromConstraint : Constraint
    {
        public FromConstraint(Guid argumentId, ValueType valueType)
            : base(argumentId, valueType)
        {
        }

        public override string Name => "From";

        public override string Text => "From";

        public override bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var answer = collectionMethodContext.GetArgumentAnswer(ValueType.ArgumentName);

            return !answer.IsNull
                       ? answer.CompareTo(ValueType) >= 0
                       : false;
        }

        public override string Description()
        {
            return string.Format("'{0}' from '{1}'", ValueType.ArgumentName, ValueType.Description());
        }

        public override IConstraint Copy()
        {
            return new FromConstraint(ArgumentId, ValueType);
        }
    }
}
