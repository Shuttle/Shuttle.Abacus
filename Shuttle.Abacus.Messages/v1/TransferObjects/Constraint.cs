using System;

namespace Shuttle.Abacus.Messages.v1.TransferObjects
{
    public class Constraint
    {
        public string ArgumentName { get; set; }
        public string ComparisonType { get; set; }
        public string Value { get; set; }
    }
}