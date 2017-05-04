using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class ChangeCalculationOrderCommand
    {
        public ChangeCalculationOrderCommand()
        {
            OrderedIds = new List<Guid>();
        }

        public Guid OwnerId { get; set; }
        public Guid MethodId { get; set; }
        public List<Guid> OrderedIds { get; private set; }
    }
}
