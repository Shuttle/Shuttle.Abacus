using System;

namespace Shuttle.Abacus.Domain
{
    public class FormulaConstraint
    {
        public FormulaConstraint(int sequenceNumber, string argumentName, string comparisonType, string value)
        {
            SequenceNumber = sequenceNumber;
            ArgumentName = argumentName;
            ComparisonType = comparisonType;
            Value = value;
        }

        public int SequenceNumber { get; }
        public string ArgumentName { get; }
        public string ComparisonType { get; }
        public string Value { get; }
    }
}