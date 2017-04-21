using System.Collections.Generic;

namespace Shuttle.Abacus.Domain
{
    public class LoginCompletedEvent 
    {
        public List<Permission> Permissions { get; set; }
    }
}
