using System;

namespace Shuttle.Abacus.Domain
{
    public class ConstraintFactory : IConstraintFactory
    {
        public IConstraint Create(string name, Guid argumentId, ArgumentAnswer argumentAnswer)
        {
            switch (name.ToLowerInvariant())
            {
                case "equals":
                {
                    return new EqualsConstraint(argumentId, argumentAnswer);
                }
                case "from":
                {
                    return new FromConstraint(argumentId, argumentAnswer);
                }
                case "notequalto":
                {
                    return new NotEqualToConstraint(argumentId, argumentAnswer);
                }
                case "to":
                {
                    return new ToConstraint(argumentId, argumentAnswer);
                }
                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}