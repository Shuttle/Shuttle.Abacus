using System;

namespace Shuttle.Abacus.Domain
{
    public class FromConstraintFactory : IConstraintFactory
    {
        public IConstraint Create(Guid argumentId, ArgumentAnswer argumentAnswer)
        {
            return new FromConstraint(argumentId, argumentAnswer);
        }

        public string Name
        {
            get { return "From"; }
        }

        public string Text
        {
            get { return "From"; }
        }
    }
}
