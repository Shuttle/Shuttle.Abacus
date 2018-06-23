using System;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class FormulaConstraint
    {
        public FormulaConstraint(Guid id, string argumentName, string comparison, string value)
        {
            Guard.AgainstNullOrEmptyString(argumentName, nameof(argumentName));
            Guard.AgainstNullOrEmptyString(comparison, nameof(comparison));
            Guard.AgainstNullOrEmptyString(value, nameof(value));

            Id = id;
            ArgumentName = argumentName;
            Comparison = comparison;
            Value = value;
        }

        public Guid Id { get; }
        public string ArgumentName { get; }
        public string Comparison { get; }
        public string Value { get; }
    }
}