using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface ISetPermissionsCommand
    {
        Guid SystemUserId { get; set; }
        List<string> PermissionIdentifiers { get; set; }
    }
}
