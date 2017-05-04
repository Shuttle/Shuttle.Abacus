using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class SetPermissionsCommand
    {
        public Guid SystemUserId { get; set; }
        public List<string> PermissionIdentifiers { get; set; }
    }
}
