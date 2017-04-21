using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class CopyMethodCommand : IMessage, ICopyMethodCommand
    {
        public Guid MethodId { get; set; }
        public string MethodName { get; set; }
    }
}
