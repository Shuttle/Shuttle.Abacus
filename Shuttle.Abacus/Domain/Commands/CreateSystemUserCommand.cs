using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class CreateSystemUserCommand 
    {
        public string LoginName { get; set; }
        public List<string> PermissionIdentifiers { get; set; }
    }
}
