using System;

namespace Shuttle.Abacus.Domain
{
    public class OwnedConstraint
    {
        public OwnedConstraint(int sequenceNumber, Guid argumentId, string name, string answer)
        {
            SequenceNumber = sequenceNumber;
            ArgumentId = argumentId;
            Name = name;
            Answer = answer;
        }

        public int SequenceNumber { get; private set; }
        public Guid ArgumentId { get; private set; }
        public string Name { get; private set; }
        public string Answer { get; private set; }
    }
}