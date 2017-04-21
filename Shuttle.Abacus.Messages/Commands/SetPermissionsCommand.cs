using System;
using System.Collections.Generic;
using Abacus.CommandMediators;
using NServiceBus;

namespace Abacus.Messages
{
    public class SetPermissionsCommand : IMessage, ISetPermissionsCommand
    {
        public Guid SystemUserId { get; set; }
        public List<string> PermissionIdentifiers { get; set; }
    }
}
