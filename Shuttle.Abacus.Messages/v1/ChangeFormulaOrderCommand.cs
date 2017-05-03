using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class ChangeFormulaOrderCommand
    {
        public ChangeFormulaOrderCommand()
        {
            OrderedIds = new List<Guid>();
        }

        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
        public List<Guid> OrderedIds { get; private set; }
    }
}
