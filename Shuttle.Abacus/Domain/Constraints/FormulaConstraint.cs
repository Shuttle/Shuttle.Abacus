using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class FormulaConstraint
    {
        private static readonly char[] separator = {','};

        public FormulaConstraint(int sequenceNumber, string argumentName, string comparison, string value)
        {
            SequenceNumber = sequenceNumber;
            ArgumentName = argumentName;
            Comparison = comparison;
            Value = value;
        }

        public int SequenceNumber { get; }
        public string ArgumentName { get; }
        public string Comparison { get; }
        public string Value { get; }

        public bool IsSatisfiedBy(ExecutionContext executionContext)
        {
            Guard.AgainstNull(executionContext, "executionContext");

            var argument = executionContext.GetArgument(ArgumentName);

            foreach (var argumentValue in executionContext.GetArgumentValue(ArgumentName).Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                var argumentValueType = argument.CreateValueType(argumentValue);

                foreach (var comparisonValue in Value.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                {
                    var comparisonValueType = argument.CreateValueType(comparisonValue);
                }
            }

            return false;
        }
    }
}