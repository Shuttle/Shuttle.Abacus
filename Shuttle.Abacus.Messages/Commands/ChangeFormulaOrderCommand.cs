using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class ChangeFormulaOrderCommand : IMessage, IChangeFormulaOrderCommand
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
