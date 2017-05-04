using System.Collections.Generic;

namespace Shuttle.Abacus.Messages.v1
{
    public class CreateSystemUserCommand 
    {
        public string LoginName { get; set; }
        public List<string> PermissionIdentifiers { get; set; }
    }
}
