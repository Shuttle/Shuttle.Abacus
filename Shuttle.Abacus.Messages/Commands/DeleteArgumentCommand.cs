using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class DeleteArgumentCommand : IMessage, IDeleteArgumentCommand
    {
        public Guid ArgumentId { get; set; }
    }
}
