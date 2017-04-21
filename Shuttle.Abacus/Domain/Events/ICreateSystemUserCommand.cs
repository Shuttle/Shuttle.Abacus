using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface ICreateSystemUserCommand
    {
        string LoginName { get; set; }
        List<string> PermissionIdentifiers { get; set; }
    }
}
