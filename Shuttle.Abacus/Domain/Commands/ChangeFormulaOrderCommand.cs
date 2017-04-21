using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class ChangeFormulaOrderCommand : IChangeFormulaOrderCommand
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
