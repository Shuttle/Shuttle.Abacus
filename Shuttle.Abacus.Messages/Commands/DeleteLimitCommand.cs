using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class DeleteLimitCommand : IMessage, IDeleteLimitCommand
    {
        public Guid LimitId { get; set; }
    }
}
