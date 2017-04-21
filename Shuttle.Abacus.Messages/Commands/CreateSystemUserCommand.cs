using System.Collections.Generic;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class CreateSystemUserCommand : IMessage, ICreateSystemUserCommand
    {
        public string LoginName { get; set; }
        public List<string> PermissionIdentifiers { get; set; }
    }
}
