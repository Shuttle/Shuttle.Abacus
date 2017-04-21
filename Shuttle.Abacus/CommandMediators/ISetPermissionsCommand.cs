using System;
using System.Collections.Generic;

namespace Abacus.CommandMediators
{
    public interface ISetPermissionsCommand
    {
        Guid SystemUserId { get; set; }
        List<string> PermissionIdentifiers { get; set; }
    }
}
