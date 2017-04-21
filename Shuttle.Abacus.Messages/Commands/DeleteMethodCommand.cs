using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class DeleteMethodCommand : IMessage, IDeleteMethodCommand
    {
        public Guid MethodId { get; set; }
    }
}
