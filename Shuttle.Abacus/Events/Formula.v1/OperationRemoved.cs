using System;

namespace Shuttle.Abacus.Events.Formula.v1
{
    public class OperationRemoved
    {
        public Guid Id { get; set; }
        public int SequenceNumber { get; set; }
    }
}