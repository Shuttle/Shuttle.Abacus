using System;

namespace Shuttle.Abacus.Domain
{
    public class ConstraintFactory : IConstraintFactory
    {
        public IConstraint Create(string name, Guid argumentId, ValueType valueType)
        {
            switch (name.ToLowerInvariant())
            {
                case "equals":
                {
                    return new EqualsConstraint(argumentId, valueType);
                }
                case "from":
                {
                    return new FromConstraint(argumentId, valueType);
                }
                case "notequalto":
                {
                    return new NotEqualToConstraint(argumentId, valueType);
                }
                case "to":
                {
                    return new ToConstraint(argumentId, valueType);
                }
                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}