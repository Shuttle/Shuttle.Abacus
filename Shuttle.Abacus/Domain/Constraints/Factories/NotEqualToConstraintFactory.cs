using System;

namespace Shuttle.Abacus.Domain
{
    public class NotEqualToConstraintFactory : IConstraintFactory
    {
        public IConstraint Create(Guid argumentId, ArgumentAnswer argumentAnswer)
        {
            return new NotEqualToConstraint(argumentId, argumentAnswer);
        }

        public string Name
        {
            get { return "NotEqualTo"; }
        }

        public string Text
        {
            get { return "Not Equal To"; }
        }
    }
}
