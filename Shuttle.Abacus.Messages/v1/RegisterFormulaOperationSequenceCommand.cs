using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterFormulaOperationSequenceCommand
    {
        public Guid FormulaId { get; set; }
        public List<Guid> SequencedIds { get; set; }
    }
}