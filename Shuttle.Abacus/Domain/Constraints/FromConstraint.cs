using System;

namespace Shuttle.Abacus.Domain
{
    public class FromConstraint : Constraint
    {
        public FromConstraint(Guid argumentId, ArgumentAnswer argumentAnswer)
            : base(argumentId, argumentAnswer)
        {
        }

        public override string Name
        {
            get { return "From"; }
        }

        public override string Text
        {
            get { return "From"; }
        }

        public override bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var answer = collectionMethodContext.GetArgumentAnswer(ArgumentAnswer.ArgumentName);

            return !answer.IsNull
                       ? answer.CompareTo(ArgumentAnswer) >= 0
                       : false;
        }

        public override string Description()
        {
            return string.Format("'{0}' from '{1}'", ArgumentAnswer.ArgumentName, ArgumentAnswer.Description());
        }

        public override IConstraint Copy()
        {
            return new FromConstraint(ArgumentId, ArgumentAnswer);
        }
    }
}
