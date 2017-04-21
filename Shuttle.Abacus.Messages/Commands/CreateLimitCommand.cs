using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class CreateLimitCommand : IMessage, ICreateLimitCommand
    {
        public string OwnerName { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
