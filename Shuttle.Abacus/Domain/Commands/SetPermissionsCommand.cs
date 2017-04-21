using System;
using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class SetPermissionsCommand
    {
        public Guid SystemUserId { get; set; }
        public List<string> PermissionIdentifiers { get; set; }
    }
}
