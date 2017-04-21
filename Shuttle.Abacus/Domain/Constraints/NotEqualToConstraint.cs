using System;

namespace Shuttle.Abacus.Domain
{
    public class NotEqualToConstraint : Constraint
    {
        public NotEqualToConstraint(Guid argumentId, ArgumentAnswer argumentAnswer)
            : base(argumentId, argumentAnswer)
        {
        }

        public override string Name
        {
            get { return "NotEqualTo"; }
        }

        public override string Text
        {
            get { return "Not Equal To"; }
        }

        public override bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            var answer = collectionMethodContext.GetArgumentAnswer(ArgumentAnswer.ArgumentName);

            return !answer.IsNull ? answer.CompareTo(ArgumentAnswer) != 0 : false;
        }

        public override string Description()
        {
            return string.Format("'{0}' not equal to '{1}'", ArgumentAnswer.ArgumentName, ArgumentAnswer.Description());
        }

        public override IConstraint Copy()
        {
            return new EqualsConstraint(ArgumentId, ArgumentAnswer);
        }
    }
}
