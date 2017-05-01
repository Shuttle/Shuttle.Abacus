using System;

namespace Shuttle.Abacus.Domain
{
    public class OwnedFormula
    {
        public OwnedFormula(Guid id, int sequenceNumber, string description)
        {
            Id = id;
            SequenceNumber = sequenceNumber;
            Description = description;
        }

        public Guid Id { get; private set; }
        public int SequenceNumber { get; private set; }
        public string Description { get; private set; }
    }
}