using System.Collections.Generic;

namespace Abacus.CommandMediators
{
    public interface ICreateSystemUserCommand
    {
        string LoginName { get; set; }
        List<string> PermissionIdentifiers { get; set; }
    }
}
