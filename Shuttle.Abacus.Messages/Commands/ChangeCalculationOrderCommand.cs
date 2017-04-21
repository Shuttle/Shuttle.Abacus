using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class ChangeCalculationOrderCommand : IMessage, IChangeCalculationOrderCommand
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
