using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class SetPermissionsCommand : ISetPermissionsCommand
    {
        public Guid SystemUserId { get; set; }
        public List<string> PermissionIdentifiers { get; set; }
    }
}
