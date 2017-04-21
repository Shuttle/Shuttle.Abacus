using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class LoginCompletedEvent 
    {
        public List<Permission> Permissions { get; set; }
    }
}
