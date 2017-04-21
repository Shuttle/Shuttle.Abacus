using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class CreateSystemUserCommand : ICreateSystemUserCommand
    {
        public string LoginName { get; set; }
        public List<string> PermissionIdentifiers { get; set; }
    }
}
