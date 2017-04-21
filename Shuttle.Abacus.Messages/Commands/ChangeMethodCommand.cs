using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class ChangeMethodCommand : IMessage, IChangeMethodCommand
    {
        public Guid MethodId { get; set; }
        public string MethodName { get; set; }
    }
}
