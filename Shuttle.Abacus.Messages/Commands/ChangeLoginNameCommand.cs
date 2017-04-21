using System;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class ChangeLoginNameCommand : IMessage, IChangeLoginNameCommand
    {
        public Guid SystemUserId { get; set; }
        public string NewLoginName { get; set; }
    }
}
