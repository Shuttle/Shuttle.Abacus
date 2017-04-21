using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class ChangeLimitCommand : IMessage, IChangeLimitCommand
    {
        public Guid LimitId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
