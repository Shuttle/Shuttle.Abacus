using System;

namespace Shuttle.Abacus
{
    public class EqualsConstraint : Constraint
    {
        public EqualsConstraint(Guid argumentId, ArgumentAnswer argumentAnswer)
            : base(argumentId, argumentAnswer)
        {
        }

        public override string Name
        {
            get { return "Equals"; }
        }

        public override string Text
        {
            get { return "Equals"; }
        }

        public override bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var answer = collectionMethodContext.GetArgumentAnswer(ArgumentAnswer.ArgumentName);

            return !answer.IsNull ? answer.CompareTo(ArgumentAnswer) == 0 : false;
        }

        public override string Description()
        {
            return string.Format("'{0}' equals '{1}'", ArgumentAnswer.ArgumentName, ArgumentAnswer.Description());
        }

        public override IConstraint Copy()
        {
            return new EqualsConstraint(ArgumentId, ArgumentAnswer);
        }
    }
}
