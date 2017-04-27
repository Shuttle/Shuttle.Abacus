using System.Collections.Generic;

namespace Shuttle.Abacus.Messages
{
    public class LoginCompletedEvent 
    {
        public List<Permission> Permissions { get; set; }
    }
}
