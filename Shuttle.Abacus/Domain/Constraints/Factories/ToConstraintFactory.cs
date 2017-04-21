using System;

namespace Shuttle.Abacus
{
    public class ToConstraintFactory : IConstraintFactory
    {
        public IConstraint Create(Guid argumentId, ArgumentAnswer argumentAnswer)
        {
            return new ToConstraint(argumentId, argumentAnswer);
        }

        public string Name
        {
            get { return "To"; }
        }

        public string Text
        {
            get { return "To"; }
        }
    }
}
