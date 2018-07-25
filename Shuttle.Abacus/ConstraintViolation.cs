using System;

namespace Shuttle.Abacus
{
    public class ConstraintViolation
    {
        public ConstraintViolation(Guid argumentId, string argumentValue, string comparison, string constraintValue)
        {
            ArgumentId = argumentId;
            ArgumentValue = argumentValue;
            Comparison = comparison;
            ConstraintValue = constraintValue;
        }

        public Guid ArgumentId { get; }
        public string ArgumentValue { get; }
        public string Comparison { get; }
        public string ConstraintValue { get; }
    }
}