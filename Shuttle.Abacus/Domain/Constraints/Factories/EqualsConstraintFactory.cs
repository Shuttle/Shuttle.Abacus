using System;

namespace Shuttle.Abacus.Domain
{
    public class EqualsConstraintFactory : IConstraintFactory
    {
        public IConstraint Create(Guid argumentId, ArgumentAnswer argumentAnswer)
        {
            return new EqualsConstraint(argumentId, argumentAnswer);
        }

        public string Name
        {
            get { return "Equals"; }
        }

        public string Text
        {
            get { return "Equals"; }
        }
    }
}
