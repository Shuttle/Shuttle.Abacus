using System;

namespace Shuttle.Abacus.Domain
{
    public class ToConstraint : Constraint
    {
        public ToConstraint(Guid argumentId, ArgumentAnswer argumentAnswer)
            : base(argumentId, argumentAnswer)
        {
        }

        public override string Name => "To";

        public override string Text => "To";

        public override bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var answer = collectionMethodContext.GetArgumentAnswer(ArgumentAnswer.ArgumentName);

            return !answer.IsNull
                       ? answer.CompareTo(ArgumentAnswer) <= 0
                       : false;
        }

        public override string Description()
        {
            return string.Format("'{0}' to '{1}'", ArgumentAnswer.ArgumentName, ArgumentAnswer.Description());
        }

        public override IConstraint Copy()
        {
            return new ToConstraint(ArgumentId, ArgumentAnswer);
        }
    }
}
