using System.Collections.Generic;
using Abacus.Infrastructure;
using NServiceBus;

namespace Abacus.Messages
{
    public class LoginCompletedEvent : IMessage
    {
        public List<Permission> Permissions { get; set; }
    }
}
